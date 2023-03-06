using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Entities.Domins.Auth
{
    public class UserLogs : BaseEntity
    {
        [Key]
        public int LogId { get; set; }

        public string LogTitle { get; set; }
        public string LogDescription { get; set; }

        public int? StoreBranchId { get; set; }
        [ForeignKey(nameof(StoreBranchId))]
        public StoreBranchs StoreBranch { get; set; }


    }
}
