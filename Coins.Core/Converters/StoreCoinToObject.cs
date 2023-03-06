using Coins.Core.Models.Domins.Auth;
using Coins.Entities.Domins.StoresInfo;
using System;

namespace Coins.Core.Converters
{
    public static class StoreCoinToObject
    {
        public static object ToMainObject(this UserStores data, Guid? userId = null)
        {
            return new
            {
                Store = new
                {
                    StoreLogoAttachmentId = data?.Store?.LogoImageId,
                    StoreLogoAttachmentUrl = !string.IsNullOrEmpty(data?.Store?.LogoImageId) ?
                        $"/File/{data.Store.LogoImageId}" : null,
                    StoreNameAr = data?.Store?.StoreNameAr,
                    StoreNameEn = data?.Store?.StoreNameEn,
                },
                NumOfVisitStore = data.NumOfVisitStore,
                TotalCoins = data.TotalCoins,
                LastVisitAt = data.LastVisitAt,
            };
        }
    }
}
