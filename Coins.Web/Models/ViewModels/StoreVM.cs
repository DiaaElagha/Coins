using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class StoreVM
    {
        [Required(ErrorMessage = "يرجى ادخال اسم المستخدم")]
        [Display(Name = "اسم المستخدم للدخول")]
        public string StoreUserName { get; set; }

        [Required(ErrorMessage = "يرجى ادخال كلمة المرور")]
        [Display(Name = "كلمة المرور للدخول")]
        [DataType(DataType.Password)]
        public string StorePassword { get; set; }

        [Required(ErrorMessage = "يرجى ادخال المتجر")]
        [Display(Name = "اسم المتجر بالعربي")]
        public string StoreNameAr { get; set; }

        [Required(ErrorMessage = "يرجى ادخال الاسم")]
        [Display(Name = "اسم المتجر بالانجليزي")]
        public string StoreNameEn { get; set; }

        [Display(Name = "وصف المتجر بالعربي")]
        public string StoreDescriptionAr { get; set; }
        [Display(Name = "وصف المتجر بالانجليزي")]
        public string StoreDescriptionEn { get; set; }

        [Required(ErrorMessage = "يرجى ادخال التصنيف")]
        [Display(Name = "تصنيف المتجر")]
        public int StoreCategoryId { get; set; }

        [Required(ErrorMessage = "يرجى ادخال نوع السعر")]
        [Display(Name = "نوع السعر")]
        public int StorePriceTypeId { get; set; }

        [Display(Name = "الحالة (الفعالية)")]
        public bool IsActive { get; set; } = false;
    }
}
