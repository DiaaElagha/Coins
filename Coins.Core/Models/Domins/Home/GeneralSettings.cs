using Coins.Entities.Domins.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.Home
{
    public class GeneralSettings : BaseEntity
    {
        [Key]
        public int SettingId { get; set; }

        public string GooglePlayMrCoinsAppLink { get; set; }
        public string AppleStoreMrCoinsAppLink { get; set; }

    }
}
