using Coins.Core.Constants.Enums;
using Coins.Core.Helpers.Models;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.DtoAPI.User;
using Coins.Entities.Domins.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Services
{
    public interface IUserService
    {
        Task<UserSocialLogin> ConnectMedia(Guid userId, SocialType socialType, string mediaToken);
        Task<Wrapper<List<Notifications>>> GetNotifications(Guid userId, int size = 15, int page = 1);
        Task<UserVouchers> GetVoucherById(int voucherId);
        Task<Wrapper<List<UserVouchers>>> Vouchers(int storeId, Guid userId, int size = 15, int page = 1);
    }
}
