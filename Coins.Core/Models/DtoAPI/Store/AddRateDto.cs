using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class AddRateDto
    {
        [Required]
        public int storeBranchId { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "The value must be between than 1-5")]
        public double rateValue { get; set; }
    }
}
