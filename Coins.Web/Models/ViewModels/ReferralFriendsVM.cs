using Coins.Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class ReferralFriendsVM
    {
        public ReferralFriendsVM()
        {
            voucherFriendReward = new VoucherFriendReward(VoucherType.FriendReward);
        }

        [Display(Name = "النقاط المكتسبة")]
        [Required(ErrorMessage = "يرجى ادخال النقاط")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public int DefineNumOfReferralCustomerCoins { get; set; }
        public bool IsActiveReferralFriend { get; set; }

        public VoucherFriendReward voucherFriendReward { get; set; }
    }
}
