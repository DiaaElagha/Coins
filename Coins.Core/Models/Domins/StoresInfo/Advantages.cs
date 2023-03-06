using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.StoresInfo
{
    public class Advantages : BaseEntity
    {
        public Advantages()
        {
            StoreBranchsAdvantagesList = new HashSet<StoreBranchsAdvantages>();
        }

        [Key]
        public int AdvantageId { get; set; }

        public string AdvantageTitleAr { get; set; }
        public string AdvantageTitleEn { get; set; }
        public string IconImageId { get; set; }

        public virtual ICollection<StoreBranchsAdvantages> StoreBranchsAdvantagesList { get; set; }

    }
}
