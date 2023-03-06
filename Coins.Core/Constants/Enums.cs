using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Constants.Enums
{
    public enum Gender { Male = 0, Female = 1 };
    public enum RewardType
    {
        [Description("عملات")]
        Coins = 0,
        [Description("قسيمة")]
        Voucher = 1
    };
    public enum VoucherStatus { Active = 0, Used = 1, NotUsed = 2, ExpiryDate = 3 };
    public enum VoucherType { Normal = 0, Discount = 1, FirstTime = 2, FriendReward = 3 };
    public enum StoreRegisterStatus { Approve = 0, Reject = 1 }
    public enum NotificationType
    {
        Action = 0,
        Remind = 1,
        System = 2
    }
    public enum SocialType
    {
        Facebook = 0,
        Instagram = 1,
        GoogleMapRate = 2
    }
    public enum HttpResponseCode
    {
        Ok = 200,
        BadRequest = 400,
        NotFound = 404,
        Unauthorized = 401,
        Forbidden = 403,
    }
}
