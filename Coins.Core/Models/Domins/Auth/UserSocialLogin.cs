using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.Auth
{
    public class UserSocialLogin : BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser UserStore { get; set; }

        [Required]
        public SocialType SocialType { get; set; }

        public string LoginToken { get; set; }
    }
}
