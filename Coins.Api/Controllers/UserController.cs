using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Coins.Api.Model;
using Coins.Api.Services;
using Coins.Api.Utilities;
using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Core.Helpers.Models;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.DtoAPI.User;
using Coins.Core.Services;
using Coins.Entities.Domins.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Coins.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IStoresService _storeService;

        public UserController(
            IAuthService authService,
            IUserService userService,
            IStoresService storeService,

            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(configuration, userManager, httpContextAccessor, mapper)
        {
            _authService = authService;
            _userService = userService;
            _storeService = storeService;
        }

        [HttpGet("/User")]
        public async Task<IActionResult> GetProfile()
            => GetResponse(ResponseMessages.READ, true, _authService.GetUserProfile(await GetCurrentUser()));

        [HttpPut("/User")]
        public async Task<IActionResult> Update([FromBody] UserProfileDto userDto)
        {
            var userId = GetCurrentUserId();
            await _authService.Update(userId, userDto, null);
            return GetResponse(ResponseMessages.UPDATE, true, userDto);
        }

        [HttpGet("/User/QR")]
        public IActionResult QR()
            => GetResponse(ResponseMessages.READ, true, ExtensionMethods.GenerateQR(GetCurrentUserId().ToString()));

        [HttpGet("/User/QR/{code}")]
        public IActionResult QR(Guid code)
            => GetResponse(ResponseMessages.READ, true, ExtensionMethods.GenerateQR(code.ToString()));

        [HttpGet("/User/StoresCoins")]
        public async Task<IActionResult> StoresCoins(int size = 15, int page = 1)
            => GetResponse(ResponseMessages.READ, true, await _storeService.GetStoresCoins(GetCurrentUserId(), size, page));

        [HttpGet("/User/Vouchers")]
        public async Task<IActionResult> Vouchers(int storeId, int size = 15, int page = 1)
        {
            var userId = GetCurrentUserId();

            var userVouchersItems = await _userService.Vouchers(storeId: storeId, userId: userId, size: size, page: page);

            var userVouchersItemsDto = _mapper.Map<List<UserVoucherDTO>>(userVouchersItems.Data);

            foreach (var item in userVouchersItemsDto)
            {
                SetVoucherStatus(item);
            }
            var resultData = new Wrapper<List<UserVoucherDTO>>
            {
                Data = userVouchersItemsDto,
                Pagination = userVouchersItems.Pagination
            };
            return GetResponse(ResponseMessages.READ, true, resultData);
        }

        [HttpGet("/User/Vouchers/{voucherId}")]
        public async Task<IActionResult> Voucher(int voucherId)
        {
            var userVouchersItem = await _userService.GetVoucherById(voucherId);
            var resultData = _mapper.Map<List<UserVoucherDTO>>(userVouchersItem);
            return GetResponse(ResponseMessages.READ, true, resultData);
        }

        [HttpPost("/User/Vouchers/Redeem/{voucherId}")]
        public async Task<IActionResult> RedeemVoucher(int voucherId)
            => GetResponse(ResponseMessages.Operation, true, await _storeService.UnlockVoucher(GetCurrentUserId(), voucherId));

        [HttpPost("/User/SocialMedia/Claim")]
        public async Task<IActionResult> SocialMediaClaim(SocialType mediaId, int storeBranchId)
            => GetResponse(ResponseMessages.Operation, true,
                await _storeService.SocialMediaActivityCliam((int)mediaId, storeBranchId, GetCurrentUserId()));

        [HttpPost("/User/SocialMedia/Connect/{media}")]
        public async Task<IActionResult> ConnectMedia(SocialType media, [FromBody] ConnectMediaDto model)
            => GetResponse(ResponseMessages.Operation, true,
                await _userService.ConnectMedia(GetCurrentUserId(), (SocialType)media, model.MediaToken));

        [HttpGet("/User/Notifications")]
        public async Task<IActionResult> Notifications(int size = 15, int page = 1)
        {
            var userId = GetCurrentUserId();
            var userNotifications = await _userService.GetNotifications(userId, size, page);
            var resultData = new Wrapper<List<NotificationDTO>>
            {
                Data = _mapper.Map<List<NotificationDTO>>(userNotifications.Data),
                Pagination = userNotifications.Pagination
            };
            return GetResponse(ResponseMessages.READ, true, resultData);
        }

        private void SetVoucherStatus(UserVoucherDTO userVoucher)
        {
            DateTimeOffset expiredDate = userVoucher.VoucherStartDate.AddDays(userVoucher.Voucher.VoucherExpiredAfterDay);
            userVoucher.VoucherExpiryDate = expiredDate;
            userVoucher.DaysLeftToExpiry = Convert.ToInt32((expiredDate - DateTimeOffset.Now).TotalDays);

            if (userVoucher.IsRedeem)
            {
                userVoucher.VoucherStatus = VoucherStatus.Used;
                return;
            }
            switch (userVoucher.DaysLeftToExpiry)
            {
                case > 0:
                    userVoucher.VoucherStatus = VoucherStatus.Active;
                    break;
                case < 0:
                    userVoucher.VoucherStatus = VoucherStatus.ExpiryDate;
                    break;
                default:
                    userVoucher.VoucherStatus = VoucherStatus.NotUsed;
                    break;
            }
            return;
        }

    }
}
