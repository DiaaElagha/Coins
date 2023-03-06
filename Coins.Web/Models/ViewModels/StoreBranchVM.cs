using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class StoreBranchObjVM
    {
        [Required(ErrorMessage = "يرجى ادخال الاسم")]
        [Display(Name = "اسم الفرع بالعربي")]
        public string BranchNameAr { get; set; }
        [Required(ErrorMessage = "يرجى ادخال الاسم")]
        [Display(Name = "اسم الفرع بالإنجليزي")]
        public string BranchNameEn { get; set; }
        [Display(Name = "وصف الفرع بالعربي")]
        public string BranchDescriptionAr { get; set; }
        [Display(Name = "وصف الفرع بالإنجليزي")]
        public string BranchDescriptionEn { get; set; }
        [Display(Name = "فرع رئيسي")]
        public bool IsMainBranch { get; set; }

        public double? BranchLatitudeLocation { get; set; }
        public double? BranchLongitudeLocation { get; set; }
    }

    public class StoreBranchVM
    {
        public StoreBranchObjVM branchVM { get; set; } = new StoreBranchObjVM();
        [Display(Name = "المميزات")]
        public int[] AdvantagesList { get; set; } = new int[] { };
    }
}
