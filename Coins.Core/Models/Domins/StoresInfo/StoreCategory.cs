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
    public class StoreCategory : BaseEntity
    {
        public StoreCategory()
        {
            StoresList = new HashSet<Stores>();
        }

        [Key]
        public int StoreCategoryId { get; set; }

        public string StoreCategoryNameAr { get; set; }
        public string StoreCategoryNameEn { get; set; }
        public string StoreCategoryAttachmentId { get; set; }

        public virtual ICollection<Stores> StoresList { get; set; }
    }
}
