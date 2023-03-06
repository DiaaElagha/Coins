using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class StoresDto
    {
        [Key]
        public int StoreId { get; set; }

        public string StoreNameAr { get; set; }
        public string StoreNameEn { get; set; }
        public string StoreDescriptionAr { get; set; }
        public string StoreDescriptionEn { get; set; }

        public int? StoreCategoryId { get; set; }
        [ForeignKey(nameof(StoreCategoryId))]
        public StoreCategoryDto StoreCategory { get; set; }

        public int? StorePriceTypeId { get; set; }
        [ForeignKey(nameof(StorePriceTypeId))]
        public StorePriceTypeDto StorePriceType { get; set; }

        public double AvgRate { get; set; }
        public string LogoImageId { get; set; }

        #region Store Settings
        //First Time Voucher
        public int? FirstTimeVoucherId { get; set; }
        [ForeignKey(nameof(FirstTimeVoucherId))]
        public VoucherDTO FirstTimeVoucher { get; set; }

        public bool IsActiveFirstTimeVoucher { get; set; }

        //Referral Customer & Friend
        public int DefineNumOfReferralCustomerCoins { get; set; }
        public int? ReferralFriendVoucherId { get; set; }
        [ForeignKey(nameof(ReferralFriendVoucherId))]
        public VoucherDTO ReferralFriendVoucher { get; set; }

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

        public virtual ICollection<SocialTypesStoreDto> SocialTypesStoresList { get; set; }
        public virtual ICollection<StoreProductDTO> StoreProductsList { get; set; }
        public virtual ICollection<VoucherDTO> VouchersList { get; set; }

    }
}
