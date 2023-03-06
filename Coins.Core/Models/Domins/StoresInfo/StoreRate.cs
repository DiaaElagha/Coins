using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.StoresInfo
{
    public class StoreRate : BaseEntity
    {
        public StoreRate()
        {
            RateValue = 0;
        }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser UserRelated { get; set; }

        public int StoreBranchId { get; set; }
        [ForeignKey(nameof(StoreBranchId))]
        public StoreBranchs StoreBranch { get; set; }

        public double RateValue { get; set; }
    }
}
