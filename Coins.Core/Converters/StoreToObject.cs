using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.CodeAnalysis;
using NetTopologySuite.Geometries;
using System;
using System.Linq;

namespace Coins.Core.Converters
{
    public static class StoreConverter
    {
        public static object ToMainObject(this StoreBranchs storeBranch, Guid? userId = null, Point pointUser = null)
        {
            return new
            {
                BranchId = storeBranch.BranchId,
                BranchNameAr = storeBranch.BranchNameAr,
                BranchNameEn = storeBranch.BranchNameEn,
                StoreId = storeBranch.StoreId,
                StoreNameAr = storeBranch?.Store?.StoreNameAr,
                StoreNameEn = storeBranch?.Store?.StoreNameEn,
                StoreCategoryNameAr = storeBranch?.Store?.StoreCategory?.StoreCategoryNameAr,
                StoreCategoryNameEn = storeBranch?.Store?.StoreCategory?.StoreCategoryNameEn,
                AvgRate = storeBranch.AvgRate,
                IsFavorite = userId != null ?
                    storeBranch.StoreUserFavoritesList.Any(c => c.UserId == userId) : false,
                StoreLogoAttachmentId = storeBranch.Store.LogoImageId,
                StoreLogoAttachmentUrl = !string.IsNullOrEmpty(storeBranch.Store.LogoImageId) ?
                    $"/File/{storeBranch.Store.LogoImageId}" : null,
                MainAttachmentId = storeBranch.BranchMainAttachmentId,
                MainAttachmentUrl = !string.IsNullOrEmpty(storeBranch.BranchMainAttachmentId) ?
                    $"/File/{storeBranch.BranchMainAttachmentId}" : null,
                Distance = pointUser != null ? (int)storeBranch.Location.Distance(pointUser) : 0,
                CreateAt = storeBranch.CreateAt.Value
            };
        }

        public static object ToFullObject(this StoreBranchs storeBranch, Guid? userId = null)
        {
            return new
            {
                BranchId = storeBranch.BranchId,
                BranchNameAr = storeBranch.BranchNameAr,
                BranchNameEn = storeBranch.BranchNameEn,
                StoreId = storeBranch.StoreId,
                StoreNameAr = storeBranch?.Store?.StoreNameAr,
                StoreNameEn = storeBranch?.Store?.StoreNameEn,
                StoreCategoryNameAr = storeBranch?.Store?.StoreCategory?.StoreCategoryNameAr,
                StoreCategoryNameEn = storeBranch?.Store?.StoreCategory?.StoreCategoryNameEn,
                AvgRate = storeBranch.AvgRate,
                IsFavorite = userId != null ?
                    storeBranch.StoreUserFavoritesList.Any(c => c.UserId == userId) : false,
                MainAttachmentId = storeBranch.BranchMainAttachmentId,
                MainAttachmentUrl = !string.IsNullOrEmpty(storeBranch.BranchMainAttachmentId) ?
                    $"/File/{storeBranch.BranchMainAttachmentId}" : null,
                Attachments = storeBranch.StoresAttachmentsList.Select(c => new
                {
                    AttachmentId = c.AttachmentsId,
                    AttachmentUrl = $"/File/{c.AttachmentsId}",
                }),
                Advantages = storeBranch.StoreBranchsAdvantagesList.Select(c => new
                {
                    AdvantageId = c.AdvantageId,
                    AdvantageTitleAr = c?.Advantage?.AdvantageTitleAr,
                    AdvantageTitleEn = c?.Advantage?.AdvantageTitleEn,
                    AttachmentId = c?.Advantage?.IconImageId,
                }),
                SocialTypes = storeBranch.Store.SocialTypesStoresList.Select(c => new
                {
                    RewardType = c.RewardType,
                    SocialType = c.SocialType,
                    VoucherId = c.VoucherId,
                    Voucher = c.VoucherId != null ? new
                    {
                        VoucherNameAr = c?.Voucher?.VoucherNameAr,
                        VoucherNameEn = c?.Voucher?.VoucherNameEn,
                        NumOfCoins = c?.Voucher?.NumOfCoins
                    } : null,
                    IsUsed = storeBranch.UserSocialStoreList.Any(x =>
                        x.SocialType == c.SocialType)
                }),
                Products = storeBranch.Store.StoreProductsList.Select(c => new
                {
                    ProductId = c.StoreProductId,
                    ProductNameAr = c.StoreProductNameAr,
                    ProductNameEn = c.StoreProductNameEn,
                    ProductPrice = c.StoreProductPrice,
                    ProductMainAttachmentId = c.StoreProductMainAttachmentId,
                }),
                Vouchers = storeBranch.Store.VouchersList.Select(c => new
                {
                    VoucherNameAr = c.VoucherNameAr,
                    VoucherNameEn = c.VoucherNameEn,
                    NumOfCoins = c.NumOfCoins
                }),
                CreateAt = storeBranch.CreateAt.Value
            };
        }

        public static object ToSliderHomeObject(this SliderHome sliderHome)
        {
            return new
            {
                Id = sliderHome.Id,
                TitleAr = sliderHome.TitleAr,
                TitleEn = sliderHome.TitleEn,
                AttachmentId = sliderHome.AttachmentId,
                ButtonTitle = sliderHome.ButtonTitle,
                StoreBranchIdButton = sliderHome.StoreBranchIdButton,
                StoreBranchNameAr = sliderHome?.StoreBranch?.BranchNameAr,
                StoreBranchNameEN = sliderHome?.StoreBranch?.BranchNameEn
            };
        }

        public static object ToAdvantageObject(this Advantages advantage)
        {
            return new
            {
                AdvantageId = advantage.AdvantageId,
                AdvantageTitleAr = advantage.AdvantageTitleAr,
                AdvantageTitleEn = advantage.AdvantageTitleEn,
                IconImageId = advantage.IconImageId,
                IconImageUrl = !string.IsNullOrEmpty(advantage.IconImageId) ? $"/File/{advantage.IconImageId}" : null,
            };
        }
    }

}
