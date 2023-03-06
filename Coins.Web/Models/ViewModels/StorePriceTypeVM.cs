using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class StorePriceTypeVM
    {
        [Required(ErrorMessage = "يرجى ادخال النوع")]
        [Display(Name = "اسم النوع بالعربي")]
        public string StorePriceTypeAr { get; set; }
        [Required(ErrorMessage = "يرجى ادخال النوع")]
        [Display(Name = "اسم النوع بالانجليزي")]
        public string StorePriceTypeEn { get; set; }
    }
}
