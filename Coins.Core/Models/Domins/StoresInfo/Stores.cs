using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.StoresInfo
{
    public class Stores : BaseEntity
    {
        public Stores()
        {
            SocialTypesStoresList = new HashSet<SocialTypesStores>();
            UserStoresList = new HashSet<UserStores>();
            StoreBranchsList = new HashSet<StoreBranchs>();
            StoreProductsList = new HashSet<StoreProducts>();
            VouchersList = new HashSet<Voucher>();
            UsersList = new HashSet<ApplicationUser>();

            IsActive = true;
            IsActive = true;

            IsActiveFirstTimeVoucher = false;
            IsActiveReferralFriend = false;

            IsActiveExpiedCoins = false;
            IsActiveMinCoinsRedeemVoucherDiscount = false;
            IsActiveMaxCoinsToSpentVoucherDiscount= false;

            IsActiveVoucherDiscount = false;
            IsActiveVoucherDiscountExpiredAfterDay = false;

            DefineCurrentCurrencyToCoins = 0;
            DefineNumOfReferralCustomerCoins = 0;
            DefineMaxCoinsToSpentVoucherDiscount = 0;
            DefineMinCoinsRedeemVoucherDiscount = 0;

        }

        [Key]
        public int StoreId { get; set; }

        public string StoreNameAr { get; set; }
        public string StoreNameEn { get; set; }
        public string StoreDescriptionAr { get; set; }
        public string StoreDescriptionEn { get; set; }

        public int? StoreCategoryId { get; set; }
        [ForeignKey(nameof(StoreCategoryId))]
        public StoreCategory StoreCategory { get; set; }

        public int? StorePriceTypeId { get; set; }
        [ForeignKey(nameof(StorePriceTypeId))]
        public StorePriceType StorePriceType { get; set; }

        public bool IsActive { get; set; }
        public bool IsPublish { get; set; }
        public string LogoImageId { get; set; }

        #region Store Settings
        //First Time Voucher
        public int? FirstTimeVoucherId { get; set; }
        [ForeignKey(nameof(FirstTimeVoucherId))]
        public Voucher FirstTimeVoucher { get; set; }

        public bool IsActiveFirstTimeVoucher { get; set; }

        //Referral Customer & Friend
        public int DefineNumOfReferralCustomerCoins { get; set; }
        public int? ReferralFriendVoucherId { get; set; }
        [ForeignKey(nameof(ReferralFriendVoucherId))]
        public Voucher ReferralFriendVoucher { get; set; }

        public bool IsActiveReferralFriend { get; set; }

        //Define Coins
        public int? ExpiedCoinsAfterDays { get; set; }
        public bool IsActiveExpiedCoins { get; set; }
        public float DefineCurrentCurrencyToCoins { get; set; }

        public bool IsActiveVoucherDiscount { get; set; }

        public int NumberRedeemedCoinsForEveryCurrency { get; set; }
        public int? VoucherDiscountExpiredAfterDay { get; set; }
        public bool IsActiveVoucherDiscountExpiredAfterDay { get; set; }
        public bool IsActiveMinCoinsRedeemVoucherDiscount { get; set; }
        public int DefineMinCoinsRedeemVoucherDiscount { get; set; }
        public bool IsActiveMaxCoinsToSpentVoucherDiscount { get; set; }
        public int DefineMaxCoinsToSpentVoucherDiscount { get; set; }

        public bool IsActiveGoogleMapRate { get; set; }
        #endregion

        public virtual ICollection<SocialTypesStores> SocialTypesStoresList { get; set; }
        public virtual ICollection<UserStores> UserStoresList { get; set; }
        public virtual ICollection<StoreBranchs> StoreBranchsList { get; set; }
        public virtual ICollection<StoreProducts> StoreProductsList { get; set; }
        public virtual ICollection<Voucher> VouchersList { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ApplicationUser> UsersList { get; set; }

    }
}
