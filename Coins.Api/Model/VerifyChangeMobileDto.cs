using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Model
{
    public class VerifyChangeMobileDto
    {
        [Required]
        public string Code { get; set; }
    }
}
