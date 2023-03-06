using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class StoreEditVM
    {

        [Required(ErrorMessage = "يرجى ادخال المتجر")]
        [Display(Name = "اسم المتجر بالعربي")]
        public string StoreNameAr { get; set; }

        [Required(ErrorMessage = "يرجى ادخال الاسم")]
        [Display(Name = "اسم المتجر بالإنجليزي")]
        public string StoreNameEn { get; set; }

        [Display(Name = "وصف المتجر بالعربي")]
        public string StoreDescriptionAr { get; set; }
        [Display(Name = "وصف المتجر بالإنجليزي")]
        public string StoreDescriptionEn { get; set; }

        [Required(ErrorMessage = "يرجى ادخال الفئة")]
        [Display(Name = "الفئة")]
        public int StoreCategoryId { get; set; }

        [Required(ErrorMessage = "يرجى ادخال معدل السعر")]
        [Display(Name = "معدل الأسعار")]
        public int StorePriceTypeId { get; set; }

        [Display(Name = "الحالة (الفعالية)")]
        public bool IsActive { get; set; } = false;

        [Display(Name = "كلمة المرور للتغيير")]
        public string Password { get; set; }
    }
}
