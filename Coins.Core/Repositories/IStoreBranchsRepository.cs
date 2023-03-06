using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Repositories
{
    public interface IStoreBranchsRepository : IRepository<StoreBranchs>
    {
        Task<StoreBranchs> GetStoreBranchById(int branchId);
        IQueryable<StoreBranchs> GetStoresNearBy(double longitude, double latitude, long meters);
        void Update(StoreBranchs storeBranch);
        void UpdateRange(List<StoreBranchs> storeBranchs);

    }
}
