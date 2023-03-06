using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class VoucherDTO
    {
        public int VoucherId { get; set; }

        public string VoucherNameAr { get; set; }
        public string VoucherNameEn { get; set; }
        public string VoucherDescrptionAr { get; set; }
        public string VoucherDescrptionEn { get; set; }
        public StoreProductDTO StoreProduct { get; set; }
        public int NumOfCoins { get; set; }
        public int VoucherExpiredAfterDay { get; set; }
        public bool IsActive { get; set; }
    }
}
