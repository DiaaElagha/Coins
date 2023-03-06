using Coins.Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class SocialStoreGMVM
    {
        public SocialStoreGMVM()
        {
            SocialType = SocialType.GoogleMapRate;
        }
        [Required(ErrorMessage = "يرجى ادخال الفرع")]
        [Display(Name = "الفرع")]
        public int StoreBranchId { get; set; }

        [Display(Name = "عدد العملات")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public int? RewardNumberOfCoins { get; set; }

        public bool IsActive { get; set; }

        public SocialType SocialType { get; set; }
    }
}
