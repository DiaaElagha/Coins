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
    public class UserStores : BaseEntity
    {
        public UserStores()
        {
            NumOfVisitStore = 0;
            TotalCoins = 0;
        }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser UserStore { get; set; }

        public int StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores Store { get; set; }

        public int TotalCoins { get; set; }

        public int NumOfVisitStore { get; set; }

        public string ReferrralCode { get; set; }
        public DateTimeOffset? LastVisitAt { get; set; }

        public int LastVisitStoreBranchId { get; set; }
        [ForeignKey(nameof(LastVisitStoreBranchId))]
        public StoreBranchs LastVisitStoreBranch { get; set; }

    }
}
