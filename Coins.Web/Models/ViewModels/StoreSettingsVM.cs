using Coins.Core.Models.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class StoreSettingsVM
    {
        [Display(Name = "جائزة اول زيارة")]
        public int? FirstTimeVoucherId { get; set; }

        [Display(Name = "تنتهي العملات المكتسبة بعد")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public int? ExpiedCoinsAfterDays { get; set; }

        [Display(Name = "قيمة العملات مقابل (1) من العملة المحلية")]
        public float DefineCurrentCurrencyToCoins { get; set; }
    }
}
