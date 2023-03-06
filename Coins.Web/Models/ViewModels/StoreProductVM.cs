using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class StoreProductVM
    {
        [Required(ErrorMessage = "يرجى ادخال المنتج")]
        [Display(Name = "اسم المنتج بالعربي")]
        public string StoreProductNameAr { get; set; }
        [Required(ErrorMessage = "يرجى ادخال المنتج")]
        [Display(Name = "اسم المنتج بالانجليزي")]
        public string StoreProductNameEn { get; set; }

        [Display(Name = "وصف المنتج بالعربي")]
        public string StoreProductDescriptionAr { get; set; }
        [Display(Name = "وصف المنتج بالانجليزي")]
        public string StoreProductDescriptionEn { get; set; }

        [Display(Name = "السعر")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public float? StoreProductPrice { get; set; }
    }
}
