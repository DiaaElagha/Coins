using AutoMapper;
using Coins.Api.Model;
using Coins.Api.Services;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Core.Models.DtoAPI.Auth;
using Coins.Core.Models.DtoAPI.User;
using Coins.Core.Services;
using Coins.Core.Services.ThiedParty;
using Coins.Entities.Domins.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Api.Repositories
{
    public class AuthRepository : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ISMSSenderService _smsSenderService;

        public AuthRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            ISMSSenderService smsSenderService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _smsSenderService = smsSenderService;
        }

        public async Task<ApplicationUser> Anonymous(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
                return null;

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.DeviceId == deviceId);

            // check if deviceId not exist
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = deviceId,
                    DeviceId = deviceId,
                    Role = UsersRoles.Anonymous
                };
                var resultCreate = await _userManager.CreateAsync(user);
            }

            {
                user.IsVerify = false;
                user.AccessToken = CreateAccess(user);
            }

            await _userManager.UpdateAsync(user);
            return user;
        }

        public async Task<ApplicationUser> VerificationUser(string mobileNo, string deviceId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.Equals(mobileNo) || x.DeviceId == deviceId);

            var code = "0000";
            //var code = new Random().Next(9999).ToString("0000");
            //call service to send this OTP code

            if (user == null)
            {
                user = await Create(new ApplicationUser { UserName = mobileNo, DeviceId = deviceId, Role = UsersRoles.Customer, IsAnonymous = false }, code);
                return user;
            }

            user.UserName = mobileNo;
            user.DeviceId = deviceId;
            await Update(user.Id, _mapper.Map<UserProfileDto>(user), code);
            return user;
        }

        public async Task<ApplicationUser> Authenticate(VerifyLoginDto loginDto)
        {
            ApplicationUser user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName.Equals(loginDto.MobileNo));
            // check if mobileNo exists
            if (user is null)
                throw new NotFoundException("Mobile number or Code is incorrect.");

            if (user.CodeHash is null || user.CodeSalt is null)
                throw new BadRequestException("Missing otp code, you must sent it before.");

            // check if code is correct
            if (!VerifyCodeHash(loginDto.Code, user.CodeHash, user.CodeSalt))
                throw new BadRequestException("Invalid otp code.");

            if (!user.IsActive)
                throw new BadRequestException("User is deactive account.");

            if (user.ExpiryDate.HasValue)
                if (user.ExpiryDate < DateTime.Now)
                    throw new BadRequestException("User is expired account.");

            // authentication successfull
            user.LastLogin = DateTimeOffset.Now;
            user.IsAnonymous = false;
            user.IsVerify = true;
            user.FcmToken = loginDto.FcmToken;
            var tokenString = CreateAccess(user);
            user.AccessToken = tokenString;
            await _userManager.UpdateAsync(user);

            await _signInManager.SignInAsync(user: user, isPersistent: false);

            return user;
        }

        public async Task Update(Guid id, UserProfileDto userDto, string code = null)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
                throw new NotFoundException(id);

            // update code if it was entered
            if (!string.IsNullOrWhiteSpace(code))
            {
                byte[] codeHash, codeSalt;
                CreateCodeHash(code, out codeHash, out codeSalt);
                user.CodeHash = codeHash;
                user.CodeSalt = codeSalt;
                await _smsSenderService.SendSMS(user.UserName, GeneralConstant.GetCodeMessage(code));
            }

            user.Birthdate = userDto.Birthdate;
            user.FullName = userDto.FullName;
            user.Gender = userDto.Gender;
            user.DeviceId = userDto.DeviceId;
            await _userManager.UpdateAsync(user);
        }

        public async Task<ApplicationUser> Create(ApplicationUser user, string code)
        {
            // validation
            if (string.IsNullOrWhiteSpace(code))
                throw new BadRequestException("Code is required");

            if (await _userManager.Users.AnyAsync(x => x.UserName.Equals(user.UserName)))
                throw new BadRequestException("MobileNo \"" + user.UserName + "\" is already taken");

            byte[] codeHash, codeSalt;
            CreateCodeHash(code, out codeHash, out codeSalt);
            user.CodeHash = codeHash;
            user.CodeSalt = codeSalt;
            await _userManager.CreateAsync(user);
            await _smsSenderService.SendSMS(user.UserName, GeneralConstant.GetCodeMessage(code));
            return user;
        }

        public UserProfileDto GetUserProfile(ApplicationUser user)
        {
            UserProfileDto userProfile = _mapper.Map<UserProfileDto>(user);
            return userProfile;
        }

        public async Task SendVerfiyMobile(ApplicationUser user, string mobileNo)
        {
            var code = "0000"; //new Random().Next(999999).ToString("000000");

            byte[] codeHash, codeSalt;
            CreateCodeHash(code, out codeHash, out codeSalt);
            user.CodeHash = codeHash;
            user.CodeSalt = codeSalt;
            await _smsSenderService.SendSMS(mobileNo, GeneralConstant.GetCodeMessage(code));

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> CheckVerfiyMobile(ApplicationUser user, string code)
        {
            // check if code is correct
            if (!VerifyCodeHash(code, user.CodeHash, user.CodeSalt))
                return false;

            // authentication successfull
            var tokenString = CreateAccess(user);
            user.UserName = user.UserName;
            user.LastLogin = DateTimeOffset.Now;
            user.UpdateAt = DateTimeOffset.Now;
            user.AccessToken = tokenString;
            user.FcmToken = user.FcmToken;
            user.IsAnonymous = false;
            await _userManager.UpdateAsync(user);

            await _signInManager.SignInAsync(user: user, isPersistent: false);
            return true;
        }


        private string CreateAccess(ApplicationUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user?.UserName ?? user.DeviceId),
                new Claim("cuserid", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(GeneralConstant.ClaimNameDeviceId, user.DeviceId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private static bool VerifyCodeHash(string code, byte[] storedHash, byte[] storedSalt)
        {
            if (code == null) throw new ArgumentNullException("code");
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "code");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of code hash (64 bytes expected).", "codeHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of code salt (128 bytes expected).", "codeHash");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(code));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
        private static void CreateCodeHash(string code, out byte[] codeHash, out byte[] codeSalt)
        {
            if (code == null) throw new ArgumentNullException("code");
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "code");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                codeSalt = hmac.Key;
                codeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(code));
            }
        }

    }
}
