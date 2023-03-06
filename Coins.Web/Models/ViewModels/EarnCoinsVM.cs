using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class EarnCoinsVM
    {
        public EarnCoinsVM()
        {
            IsActiveExpiedCoins = false;
            DefineCurrentCurrencyToCoins = 0;

            SocialStores = new List<SocialStoreVM>();
            SocialStoresGoogleMap = new List<SocialStoreGMVM>();
        }
        public List<SocialStoreVM> SocialStores { get; set; }
        public List<SocialStoreGMVM> SocialStoresGoogleMap { get; set; }

        public bool IsActiveExpiedCoins { get; set; }

        [Required(ErrorMessage = "يرجى ادخال قيمة النقاط")]
        [Display(Name = "النقاط المكتسبة مقابل كل دينار يتم انفاقه")]
        public float DefineCurrentCurrencyToCoins { get; set; }

        public int? ExpiedCoinsAfterDays { get; set; }

        public bool IsActiveGoogleMapRate { get; set; }
    }
}
