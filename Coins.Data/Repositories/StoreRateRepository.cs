using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Repositories;
using Coins.Data.Data;
using Coins.Data.Repositories;

using Coins.Entities.Domins.Notification;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories
{
    public class StoreRateRepository : Repository<StoreRate>, IStoreRateRepository
    {
        public StoreRateRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(StoreRate storeRate)
        {
            Context.Update(storeRate);
        }

        public async Task<double> GetStoreBranchRate(int storeBranchId)
        {
            double RatingAverage = await Context.StoreRate.Where(r => r.StoreBranchId == storeBranchId).AverageAsync(r => r.RateValue);
            return RatingAverage;
        }

    }
}
