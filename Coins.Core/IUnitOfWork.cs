using Coins.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAdvantagesRepository Advantages { get; }
        IContactUsRepository ContactUs { get; }
        IGeneralSettingsRepository GeneralSettings { get; }
        IMessageSMSRepository MessageSMS { get; }
        IMrCoinsSocialMediaRepository MrCoinsSocialMedia { get; }
        INotificationsRepository Notifications { get; }
        ISocialTypesStoresRepository SocialTypesStores { get; }
        IStoreBranchsAdvantagesRepository StoreBranchsAdvantages { get; }
        IStoreBranchsRepository StoreBranchs { get; }
        IStoreCategoryRepository StoreCategory { get; }
        IStoreProductsRepository StoreProducts { get; }
        IStoreRateRepository StoreRate { get; }
        IStoresBranchsAttachmentsRepository StoresBranchsAttachments { get; }
        IStoresProductsAttachmentsRepository StoresProductsAttachments { get; }
        IStoresRepository Stores { get; }
        IUserCoinsRepository UserCoins { get; }
        IUserVouchersRepository UserVouchers { get; }
        IUserLogsRepository UserLogs { get; }
        IUserStoresRepository UserStores { get; }
        IVouchersRepository Vouchers { get; }
        IStorePriceTypeRepository StorePriceType { get; }
        IUserSocialStoreRepository UserSocialStore { get; }
        ILocationTestRepositiry LocationTestRepository { get; }
        IUserSocialLoginRepository UserSocialLogin { get; }
        IStoreFavoriteRepository StoreFavorites { get; }
        ISliderHomeRepository sliderHome { get; }

        Task<int> CommitAsync();
        int Commit();
    }
}
