using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class AdvantagesVM
    {
        [Required(ErrorMessage = "يرجى ادخال الميزة")]
        [Display(Name = "اسم الميزة AR")]
        public string AdvantageTitleAr { get; set; }

        [Required(ErrorMessage = "يرجى ادخال الميزة")]
        [Display(Name = "اسم الميزة EN")]
        public string AdvantageTitleEn { get; set; }

    }
}
