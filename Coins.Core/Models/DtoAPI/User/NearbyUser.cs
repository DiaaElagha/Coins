using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.User
{
    public class NearbyUser
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public long Distance { get; set; }
    }
}
