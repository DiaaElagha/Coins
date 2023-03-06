using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.Social
{
    public class MrCoinsSocialMedia : BaseEntity
    {
        public MrCoinsSocialMedia()
        {

        }

        [Key]
        public int MrCoinsSocialMediaId { get; set; }

        public SocialType SocialType { get; set; }

        public string UrlLink { get; set; }
    }
}
