using AutoMapper;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers.Models;
using Coins.Core.Models.Domins.Attachments;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.DtoAPI.User;
using Coins.Core.Services;
using Coins.Core.Services.ThiedParty;
using Coins.Entities.Domins.Notification;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationsService _notificationService;
        public UserService(IUnitOfWork unitOfWork, INotificationsService notificationService)
        {
            this._unitOfWork = unitOfWork;
            this._notificationService = notificationService;
        }

        public async Task<Wrapper<List<UserVouchers>>> Vouchers(int storeId, Guid userId, int size = 15, int page = 1)
        {
            if (!await _unitOfWork.Stores.Get().AnyAsync(x => x.StoreId == storeId))
                throw new NotFoundException(storeId);

            var skip = size * (page - 1);
            var IQueryableVouchers = _unitOfWork.UserVouchers.Get()
                .Where(x => (!x.IsDeleted) && (x.UserId == userId) && (x.Voucher.StoreId == storeId))
                .OrderByDescending(v => v.CreateAt)
                .Include(v => v.Voucher);

            var totalItems = await IQueryableVouchers.CountAsync();

            var listVouchers = await IQueryableVouchers
                .Skip(skip)
                .Take(size)
                .ToListAsync();
            return new Wrapper<List<UserVouchers>>
            {
                Data = listVouchers,
                Pagination = new Pagination(size, totalItems)
            };
        }

        public async Task<UserVouchers> GetVoucherById(int voucherId)
        {
            var userVoucher = await _unitOfWork.UserVouchers.Get()
                .Include(v => v.Voucher).SingleOrDefaultAsync(x => x.VoucherId == voucherId);
            if (userVoucher is null)
                throw new NotFoundException(voucherId);
            return userVoucher;
        }

        public async Task<UserSocialLogin> ConnectMedia(Guid userId, SocialType socialType, string mediaToken)
        {
            var itemUserSocialLogin = new UserSocialLogin
            {
                LoginToken = mediaToken,
                SocialType = socialType,
                UserId = userId,
                CreateByUserId = userId,
                CreateAt = DateTime.Now,
            };
            await _unitOfWork.UserSocialLogin.AddAsync(itemUserSocialLogin);
            await _unitOfWork.CommitAsync();
            return itemUserSocialLogin;
        }

        public async Task<Wrapper<List<Notifications>>> GetNotifications(Guid userId, int size = 15, int page = 1)
        {
            var skip = size * (page - 1);
            var IQueryableUserNotifications = _unitOfWork.Notifications.Get()
                .Where(x => x.ReceverId == userId)
                .OrderByDescending(v => v.SendDateAt);

            var totalItems = await IQueryableUserNotifications.CountAsync();

            var listUserNotifications = await IQueryableUserNotifications
                .Skip(skip)
                .Take(size)
                .ToListAsync();
            return new Wrapper<List<Notifications>>
            {
                Data = listUserNotifications,
                Pagination = new Pagination(size, totalItems)
            };
        }

    }
}
