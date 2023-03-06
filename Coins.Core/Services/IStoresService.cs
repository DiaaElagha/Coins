using Coins.Core.Helpers.Models;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.DtoAPI.Store;
using Coins.Core.Models.DtoAPI.User;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Services
{
    public interface IStoresService
    {
        Task<double> AddRate(Guid userId, int storeBranchId, double rateValue);
        Task<bool> SocialMediaActivityCliam(int mediaId, int storeBranchId, Guid userId);
        Task<Stores> CreateStore(Stores stores, string userName, string password);
        Task<object> GetStoreById(Guid userId, int storeBranchId, bool addToSearch);
        Task<Wrapper<List<StoreCategory>>> GetStoreCategories(int size, int page);
        Task<Wrapper<List<StoreProducts>>> GetStoreMenu(int storeId, int size, int page);
        Task<bool> SetInvoice(Guid cashierId, SetInvoiceDto model);
        Task<bool> UnlockVoucher(Guid userId, int voucherId);
        Task<Wrapper<List<object>>> GetStoresMostVisited(StoresParamDto model);
        Task<object> GetStoreAds(StoresParamAdsDto model);
        Task<Wrapper<List<object>>> GetStoresVisitedBefore(Guid userId, StoresParamDto model);
        Task<Wrapper<List<object>>> GetFirstTimeVouchers(int size, int page);
        Task<Wrapper<List<object>>> GetStoresMain(Guid userId, StoresParamMainDto model);
        object CheckStoreData(int storeId);
        Task<object> GetSliderHome();
        Task ChangeFavorite(Guid userId, int storeBranchId);
        Task<object> GetAdvantages(int storeBranchId, int size, int page);
        Task<object> GetStoresCoins(Guid userId, int size, int page);
    }
}
