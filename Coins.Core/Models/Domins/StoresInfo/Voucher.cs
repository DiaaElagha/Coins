using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.StoresInfo
{
    public class Voucher : BaseEntity
    {
        public Voucher()
        {
            NumOfCoins = 0;
            VoucherExpiredAfterDay = 90;
            IsActive = true;
        }

        [Key]
        public int VoucherId { get; set; }

        public string VoucherNameAr { get; set; }
        public string VoucherNameEn { get; set; }
        public string VoucherDescrptionAr { get; set; }
        public string VoucherDescrptionEn { get; set; }

        public bool IsActive { get; set; }

        public int? StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores Store { get; set; }

        public int NumOfCoins { get; set; }

        public int VoucherExpiredAfterDay { get; set; }

        public string VoucherMainAttachmentId { get; set; }

        public float? VoucherDiscountValue { get; set; }
        public string VoucherTerms { get; set; }

        [Required]
        public VoucherType VoucherType { get; set; }
    }
}
