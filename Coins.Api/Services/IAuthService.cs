using Coins.Api.Model;
using Coins.Core.Models.DtoAPI.Auth;
using Coins.Core.Models.DtoAPI.User;
using Coins.Entities.Domins.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Api.Services
{
    public interface IAuthService
    {
        Task<ApplicationUser> Anonymous(string deviceId);
        Task<ApplicationUser> Authenticate(VerifyLoginDto loginDto);
        Task<bool> CheckVerfiyMobile(ApplicationUser user, string code);
        Task<ApplicationUser> Create(ApplicationUser user, string code);
        UserProfileDto GetUserProfile(ApplicationUser user);
        Task SendVerfiyMobile(ApplicationUser user, string mobileNo);
        Task Update(Guid id, UserProfileDto userDto, string code = null);
        Task<ApplicationUser> VerificationUser(string mobileNo, string deviceId);
    }
}
