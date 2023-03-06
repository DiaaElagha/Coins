using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Constants
{
    public class UsersRoles
    {
        public const string Admin = "Admin";
        public const string StoreAdmin = "StoreAdmin";
        public const string Casher = "Casher";
        public const string Customer = "Customer";
        public const string Anonymous = "Anonymous";
    }


    public class GeneralConstant
    {
        public const string ClaimNameDeviceId = "DeviceId";


        public static string GetCodeMessage(string code)
        {
            return $"Your verification code is: {code}";
        }


        public const string VoucherFirstTimeTerms = 
            "تستخدم هذه القسيمة لمرة واحد فقط " + "\n" +
            "لا يمكن استبدال القسيمة بمبلغ نقدي أو استردادها أو اعادة بيعها" + "\n" +
            "تنتهي صلاحية القسيمة بعد إتمام اول طلب" + "\n" +
            "لا يمكن استبدال القسيمة بأي مكافأة اخرى";

        public const string VoucherFriendRewardTerms =
           "تستخدم هذه القسيمة لمرة واحد فقط " + "\n" +
           "لا يمكن استبدال القسيمة بمبلغ نقدي أو استردادها أو اعادة بيعها" + "\n" +
           "تنتهي صلاحية القسيمة بعد إتمام اول طلب" + "\n" +
           "لا يمكن استبدال القسيمة بأي مكافأة اخرى";
    }


}
