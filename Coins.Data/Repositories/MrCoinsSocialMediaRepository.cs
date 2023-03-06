using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Repositories;
using Coins.Data.Data;
using Coins.Data.Repositories;

using Coins.Entities.Domins.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories
{
    public class MrCoinsSocialMediaRepository : Repository<MrCoinsSocialMedia>, IMrCoinsSocialMediaRepository
    {
        public MrCoinsSocialMediaRepository(ApplicationDbContext context)
                : base(context)
        { }




    }
}
