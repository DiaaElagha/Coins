using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Model
{
    public class VerifyLoginDto
    {
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string FcmToken { get; set; }
    }
}
