using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Repositories;
using Coins.Data.Data;
using Coins.Data.Repositories;

using Coins.Entities.Domins.Notification;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories
{
    public class StoresRepository : Repository<Stores>, IStoresRepository
    {
        public StoresRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(Stores store)
        {
            Context.Update(store);
        }

        public async Task<Stores> GetStoreWithItemsById(int storeId)
        {
            return await Context.Stores.Where(x => x.StoreId == storeId)
                .AsNoTracking().AsQueryable()
                .Include(x => x.StoreCategory)
                .Include(x => x.SocialTypesStoresList)
                .Include(x => x.StoreBranchsList)
                .Include(x => x.StoreProductsList)
                .Include(x => x.VouchersList)
                .SingleOrDefaultAsync();
        }

    }
}
