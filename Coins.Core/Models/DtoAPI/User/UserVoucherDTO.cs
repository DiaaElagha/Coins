using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.DtoAPI.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.User
{
    public class UserVoucherDTO
    {
        public VoucherDTO Voucher { get; set; }

        public VoucherStatus VoucherStatus { get; set; }

        public DateTimeOffset VoucherStartDate { get; set; }
        public DateTimeOffset VoucherExpiryDate { get; set; }
        public int? DaysLeftToExpiry { get; set; }
        public bool IsRedeem { get; set; }
    }
}
