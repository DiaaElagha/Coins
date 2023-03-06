using Coins.Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class SocialTypesStoreDto
    {
        public int StoreBranchId { get; set; }
        public int StoreId { get; set; }
        public SocialType SocialType { get; set; }
        public RewardType RewardType { get; set; }
        public string UrlLink { get; set; }
        public int? RewardNumberOfCoins { get; set; }
        public int? VoucherId { get; set; }
        public VoucherDTO Voucher { get; set; }
        public bool IsActive { get; set; }
    }
}
