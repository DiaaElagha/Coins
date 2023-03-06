using Coins.Core.Helpers;
using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.DtoAPI.User;
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
    public class StoreBranchsRepository : Repository<StoreBranchs>, IStoreBranchsRepository
    {
        public StoreBranchsRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void UpdateRange(List<StoreBranchs> storeBranchs)
        {
            Context.UpdateRange(storeBranchs);
        }

        public void Update(StoreBranchs storeBranch)
        {
            Context.Update(storeBranch);
        }

        public IQueryable<StoreBranchs> GetStoresNearBy(double longitude, double latitude, long meters)
        {
            var minMilePerLat = 68.703;
            var milePerLon = Math.Cos(longitude) * 69.172;

            var minLat = latitude - meters / minMilePerLat;
            var maxLat = latitude + meters / minMilePerLat;
            var minLon = longitude - meters / milePerLon;
            var maxLon = longitude + meters / milePerLon;

            var listStoreBranchs = Context.StoreBranchs.Where(
                x => (!x.IsDeleted) &&
                         (minLat <= x.BranchLatitudeLocation && x.BranchLatitudeLocation <= maxLat) &&
                         (minLon <= x.BranchLongitudeLocation && x.BranchLongitudeLocation <= maxLon));

            //var listStoreBranchs = Context.StoreBranchs.Where(
            //    x => (!x.IsDeleted));
            return listStoreBranchs;
        }

        public async Task<StoreBranchs> GetStoreBranchById(int branchId)
        {
            return await Context.StoreBranchs.Where(x => x.BranchId == branchId)
                .Include(x => x.StoresAttachmentsList)
                .Include(x => x.StoreBranchsAdvantagesList)
                .ThenInclude(x => x.Advantage)
                .Include(x => x.Store)
                .ThenInclude(x => x.StoreCategory)
                .Include(x => x.Store.SocialTypesStoresList)
                .Include(x => x.Store.StoreProductsList)
                .Include(x => x.Store.VouchersList)
                .SingleOrDefaultAsync();
        }

    }
}
