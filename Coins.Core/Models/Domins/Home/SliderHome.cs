using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.Home
{
    public class SliderHome : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string AttachmentId { get; set; }

        public string ButtonTitle { get; set; }

        public int? StoreBranchIdButton { get; set; }
        [ForeignKey(nameof(StoreBranchIdButton))]
        public StoreBranchs StoreBranch { get; set; }
    }
}
