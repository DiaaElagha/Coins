using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class SliderHomeVM
    {
        [Required(ErrorMessage = "يرجى ادخال العنوان")]
        [Display(Name = "العنوان AR")]
        public string TitleAr { get; set; }

        [Required(ErrorMessage = "يرجى ادخال العنوان")]
        [Display(Name = "العنوان EN")]
        public string TitleEn { get; set; }

        [Display(Name = "المتجر")]
        public int? StoreBranchIdButton { get; set; }

        [Display(Name = "نص الزر")]
        public string ButtonTitle { get; set; }

    }
}
