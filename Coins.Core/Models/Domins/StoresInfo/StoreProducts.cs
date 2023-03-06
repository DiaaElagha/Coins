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
    public class StoreProducts : BaseEntity
    {
        public StoreProducts()
        {
            StoresList = new HashSet<Stores>();
            StoresProductsAttachmentsList = new HashSet<StoresProductsAttachments>();
            VouchersList = new HashSet<Voucher>();

            StoreProductPrice = 0;
        }

        [Key]
        public int StoreProductId { get; set; }

        public string StoreProductNameAr { get; set; }
        public string StoreProductNameEn { get; set; }
        public string StoreProductDescriptionAr { get; set; }
        public string StoreProductDescriptionEn { get; set; }
        public int StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores Store { get; set; }
        public float? StoreProductPrice { get; set; }
        public string StoreProductMainAttachmentId { get; set; }

        public virtual ICollection<Stores> StoresList { get; set; }
        public virtual ICollection<StoresProductsAttachments> StoresProductsAttachmentsList { get; set; }
        public virtual ICollection<Voucher> VouchersList { get; set; }
    }
}
