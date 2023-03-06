using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class StoreCategoryVM
    {
        [Required(ErrorMessage = "يرجى ادخال التصنيف")]
        [Display(Name = "اسم التصنيف بالعربي")]
        public string StoreCategoryNameAr { get; set; }

        [Required(ErrorMessage = "يرجى ادخال التصنيف")]
        [Display(Name = "اسم التصنيف بالانجليزي")]
        public string StoreCategoryNameEn { get; set; }

        public string StoreCategoryAttachmentId { get; set; }
    }
}
