using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Helpers.Models
{
    public class SearchParametersEntity
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
        public string Props { get; set; }
        public bool IsUpdating { get; set; }

        public override string ToString()
        {
            return $"{From.ToString("yyyy-MM-dd")} => {To.ToString("yyyy-MM-dd")}";
        }
    }
}
