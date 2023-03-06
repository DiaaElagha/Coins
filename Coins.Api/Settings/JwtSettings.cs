using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Settings
{
    public class JwtSettings
    {
        public static string Issuer;
        public static string Secret;
        public static int ExpirationInDays;
    }
}
