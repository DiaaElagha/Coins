using Coins.Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class VoucherVM
    {
        [Required(ErrorMessage = "يرجى ادخال القسيمة")]
        [Display(Name = "اسم القسيمة بالعربي")]
        public string VoucherNameAr { get; set; }

        [Required(ErrorMessage = "يرجى ادخال القسيمة")]
        [Display(Name = "اسم القسيمة بالانجليزي")]
        public string VoucherNameEn { get; set; }

        [Display(Name = "وصف القسيمة بالعربي")]
        public string VoucherDescrptionAr { get; set; }
        [Display(Name = "وصف القسيمة بالانجليزي")]
        public string VoucherDescrptionEn { get; set; }

        [Display(Name = "الحالة (الفعالية)")]
        public bool IsActive { get; set; } = false;

        [Required(ErrorMessage = "يرجى ادخال عدد النقاط")]
        [Display(Name = "عدد النقاط")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public int NumOfCoins { get; set; }

        [Display(Name = "تنتهي القسيمة المكتسبة بعد x يوم")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public int VoucherExpiredAfterDay { get; set; }

    }
}
