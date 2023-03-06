using Coins.Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Helpers.Models
{
    public class FacebookUserResource
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public Gender? Gender { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public List<string> Friends { get; set; }
    }
}
