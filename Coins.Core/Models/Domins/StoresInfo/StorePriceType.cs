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
    public class StorePriceType : BaseEntity
    {
        public StorePriceType()
        {
            StoresList = new HashSet<Stores>();
        }

        [Key]
        public int StorePriceTypeId { get; set; }

        public string StorePriceTypeAr { get; set; }
        public string StorePriceTypeEn { get; set; }

        public virtual ICollection<Stores> StoresList { get; set; }
    }
}
