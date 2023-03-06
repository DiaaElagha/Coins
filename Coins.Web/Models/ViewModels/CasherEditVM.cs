using Coins.Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class CasherEditVM
    {
        [Required(ErrorMessage = "يرجى ادخال الاسم")]
        [Display(Name = "الاسم كامل")]
        public string FullName { get; set; }
        public string Address { get; set; }

        [Display(Name = "الجنس")]
        public Gender? Gender { get; set; }

        [Display(Name = "الحالة (الفعالية)")]
        public bool IsActive { get; set; } = false;

        [Display(Name = "تاريخ الانتهاء")]
        [DataType(DataType.Date)]
        public DateTimeOffset? ExpiryDate { get; set; }
    }
}
