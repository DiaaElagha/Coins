using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Services.ThiedParty
{
    public interface ISMSSenderService
    {
        Task<bool> SendSMS(string to, string msg);
    }
}
