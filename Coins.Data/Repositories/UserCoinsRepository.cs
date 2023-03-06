using Coins.Core.Models.Domins.Auth;
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
    public class UserCoinsRepository : Repository<UserCoins>, IUserCoinsRepository
    {
        public UserCoinsRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(UserCoins userCoin)
        {
            Context.Update(userCoin);
        }

        public async Task<int> GetSumCoins(Guid userId, Stores store)
        {
            int sumOfCoins = 0;
            if (store.IsActiveExpiedCoins && store.ExpiedCoinsAfterDays.HasValue)
            {
                sumOfCoins = await Context.UserCoins.Where(x =>
                    (x.UserId == userId && x.StoreId == store.StoreId))
                    .SumAsync(x => x.Remaining);
            }
            else
            {
                sumOfCoins = await Context.UserCoins.Where(x =>
                   (x.UserId == userId && x.StoreId == store.StoreId)).SumAsync(x => x.NumberOfCoinCollected);
            }
            return sumOfCoins;
        }

    }
}
