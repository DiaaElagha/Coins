using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.Auth
{
    public class UserVouchers : BaseEntity
    {
        public UserVouchers()
        {
            VoucherStartDate = DateTimeOffset.Now;
            VoucherStatus = VoucherStatus.Active;
            IsRedeem = false;
        }

        [Key]
        public int UserVoucherId { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser UserRelated { get; set; }

        public int VoucherId { get; set; }
        [ForeignKey(nameof(VoucherId))]
        public Voucher Voucher { get; set; }

        public VoucherStatus VoucherStatus { get; set; }

        public DateTimeOffset VoucherStartDate { get; set; }

        public bool IsRedeem { get; set; }

    }
}
