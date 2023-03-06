using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Model
{
    public class BaseUserProfileDto
    {
        public string DeviceId { get; set; }
        public string MobileNo { get; set; }
        public string AccessToken { get; set; }
    }
}
