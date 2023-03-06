using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.StoresInfo
{
    public class StoresProductsAttachments : BaseEntity
    {
        [Key]
        public int StoreProductAttachmentId { get; set; }

        [Required]
        public string AttachmentsId { get; set; }

        [Required]
        public int StoreProductId { get; set; }
        [ForeignKey(nameof(StoreProductId))]
        public StoreProducts StoreProduct { get; set; }

    }
}
