using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Model
{
    public class ChangeMobileDto
    {
        [Required]
        public string MobileNo { get; set; }
    }
}
