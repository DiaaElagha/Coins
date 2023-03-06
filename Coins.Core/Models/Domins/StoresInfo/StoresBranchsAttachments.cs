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
    public class StoresBranchsAttachments : BaseEntity
    {
        [Key]
        public int StoreBranchAttachmentId { get; set; }

        [Required]
        public string AttachmentsId { get; set; }

        [Required]
        public int StoreBranchId { get; set; }
        [ForeignKey(nameof(StoreBranchId))]
        public StoreBranchs StoreBranch { get; set; }

    }
}
