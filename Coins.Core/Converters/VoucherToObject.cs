using Coins.Core.Models.Domins.StoresInfo;
using System;

namespace Coins.Core.Converters
{
    public static class VoucherConverter
    {
        public static object ToMainObject(this Voucher voucher, Guid? userId = null)
        {
            return new
            {
                VoucherId = voucher.VoucherId,
                VoucherNameAr = voucher.VoucherNameAr,
                VoucherNameEn = voucher.VoucherNameEn,
                VoucherTerms = voucher.VoucherTerms,
                NumOfCoins = voucher.NumOfCoins,
                Store = new
                {
                    StoreId = voucher.Store.StoreId,
                    StoreNameAr = voucher.Store.StoreNameAr,
                    StoreNameEn = voucher.Store.StoreNameEn,
                }
            };
        }
    }
}
