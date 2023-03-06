using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class StoresParamDto
    {
        public StoresParamDto()
        {
            size = 15;
            page = 1;
        }
        public double? userLongitude { get; set; }
        public double? userLatitude { get; set; }
        public double? distance { get; set; }

        public string search { get; set; }
        public int? categoryId { get; set; }
        public int[] services { get; set; }
        public int size { get; set; }
        public int page { get; set; }
    }
}
