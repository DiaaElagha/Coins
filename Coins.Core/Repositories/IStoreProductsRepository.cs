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
    public interface IStoreProductsRepository : IRepository<StoreProducts>
    {
        void Update(StoreProducts storeProduct);

    }
}
