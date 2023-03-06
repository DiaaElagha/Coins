using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class StoresParamAdsDto
    {
        public StoresParamAdsDto() { }
        [Required]
        public double userLongitude { get; set; }
        [Required]
        public double userLatitude { get; set; }
        [Required]
        public double distance { get; set; }
        [Required]
        public int categoryId { get; set; }
    }
}
