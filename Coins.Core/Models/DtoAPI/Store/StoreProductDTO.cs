using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class StoreProductDTO
    {
        public int StoreProductId { get; set; }
        public string StoreProductNameAr { get; set; }
        public string StoreProductNameEn { get; set; }
        public string StoreProductDescriptionAr { get; set; }
        public string StoreProductDescriptionEn { get; set; }
        public float? StoreProductPrice { get; set; }
        public string StoreProductMainAttachmentId { get; set; }

        public int StoreId { get; set; }
        public StoresDto Store { get; set; }
    }
}
