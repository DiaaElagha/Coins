using Coins.Core.Services;
using Coins.Core.Services.ThiedParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories.ThiedParty
{
    public class SMSSenderRepository : ISMSSenderService
    {
        public async Task<bool> SendSMS(string to, string msg)
        {
            //ToDo: Implement sms send
            return true;
        }

    }
}
