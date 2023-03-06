using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.Social
{
    public class SocialTypesStores : BaseEntity
    {
        public SocialTypesStores()
        {
            IsUsed = false;
            IsActive = true;
            RewardType = RewardType.Coins;
            RewardNumberOfCoins = 0;
        }

        public int StoreBranchId { get; set; }
        [ForeignKey(nameof(StoreBranchId))]
        public StoreBranchs StoreBranch { get; set; }

        public int StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores Store { get; set; }

        [Required]
        public SocialType SocialType { get; set; }

        public RewardType RewardType { get; set; }

        public string UrlLink { get; set; }
        public int? RewardNumberOfCoins { get; set; }

        public int? VoucherId { get; set; }
        [ForeignKey(nameof(VoucherId))]
        public Voucher Voucher { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        public bool IsUsed { get; set; }


    }
}
