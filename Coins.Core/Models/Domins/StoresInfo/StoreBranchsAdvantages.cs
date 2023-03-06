using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Entities.Domins.StoresInfo
{
    public class StoreBranchsAdvantages : BaseEntity
    {
        public StoreBranchsAdvantages()
        {

        }

        public int AdvantageId { get; set; }
        [ForeignKey(nameof(AdvantageId))]
        public Advantages Advantage { get; set; }

        public int StoreBranchId { get; set; }
        [ForeignKey(nameof(StoreBranchId))]
        public StoreBranchs StoreBranch { get; set; }
    }
}
