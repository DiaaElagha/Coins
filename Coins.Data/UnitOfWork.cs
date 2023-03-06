using Coins.Core;
using Coins.Core.Repositories;
using Coins.Data.Data;
using Coins.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private AdvantagesRepository _advantagesRepository;
        private ContactUsRepository _contactUsRepository;
        private GeneralSettingsRepository _generalSettingsRepository;
        private MessageSMSRepository _messageSMSRepository;
        private MrCoinsSocialMediaRepository _mrCoinsSocialMediaRepository;
        private NotificationsRepository _notificationsRepository;
        private SocialTypesStoresRepository _socialTypesStoresRepository;
        private StoreBranchsAdvantagesRepository _storeBranchsAdvantagesRepository;
        private StoreBranchsRepository _storeBranchsRepository;
        private StoreCategoryRepository _storeCategoryRepository;
        private StorePriceTypeRepository _storePriceTypeRepository;
        private StoreProductsRepository _storeProductsRepository;
        private StoreRateRepository _storeRateRepository;
        private StoreFavoriteRepository _storeFavoriteRepository;
        private StoresBranchsAttachmentsRepository _storesBranchsAttachments;
        private StoresProductsAttachmentsRepository _storesProductsAttachments;
        private StoresRepository _storesRepository;
        private UserCoinsRepository _userCoinsRepository;
        private UserVouchersRepository _userVouchersRepository;
        private UserLogsRepository _userLogsRepository;
        private UserStoresRepository _userStoresRepository;
        private UserSocialStoreRepository _userSocialStoreRepository;
        private UserSocialLoginRepository _userSocialLoginRepository;
        private VouchersRepository _vouchersRepository;
        private SliderHomeRepository _sliderHomeRepository;

        private LocationTestRepository _locationTestRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IAdvantagesRepository Advantages => _advantagesRepository = _advantagesRepository ?? new AdvantagesRepository(_context);
        public IContactUsRepository ContactUs => _contactUsRepository = _contactUsRepository ?? new ContactUsRepository(_context);
        public IGeneralSettingsRepository GeneralSettings => _generalSettingsRepository = _generalSettingsRepository ?? new GeneralSettingsRepository(_context);
        public IMessageSMSRepository MessageSMS => _messageSMSRepository = _messageSMSRepository ?? new MessageSMSRepository(_context);
        public IMrCoinsSocialMediaRepository MrCoinsSocialMedia => _mrCoinsSocialMediaRepository = _mrCoinsSocialMediaRepository ?? new MrCoinsSocialMediaRepository(_context);
        public INotificationsRepository Notifications => _notificationsRepository = _notificationsRepository ?? new NotificationsRepository(_context);
        public ISocialTypesStoresRepository SocialTypesStores => _socialTypesStoresRepository = _socialTypesStoresRepository ?? new SocialTypesStoresRepository(_context);
        public IStoreBranchsAdvantagesRepository StoreBranchsAdvantages => _storeBranchsAdvantagesRepository = _storeBranchsAdvantagesRepository ?? new StoreBranchsAdvantagesRepository(_context);
        public IStoreBranchsRepository StoreBranchs => _storeBranchsRepository = _storeBranchsRepository ?? new StoreBranchsRepository(_context);
        public IStoreCategoryRepository StoreCategory => _storeCategoryRepository = _storeCategoryRepository ?? new StoreCategoryRepository(_context);
        public IStorePriceTypeRepository StorePriceType => _storePriceTypeRepository = _storePriceTypeRepository ?? new StorePriceTypeRepository(_context);
        public IStoreProductsRepository StoreProducts => _storeProductsRepository = _storeProductsRepository ?? new StoreProductsRepository(_context);
        public IStoreRateRepository StoreRate => _storeRateRepository = _storeRateRepository ?? new StoreRateRepository(_context);
        public IStoreFavoriteRepository StoreFavorites => _storeFavoriteRepository = _storeFavoriteRepository ?? new StoreFavoriteRepository(_context);
        public IStoresBranchsAttachmentsRepository StoresBranchsAttachments => _storesBranchsAttachments = _storesBranchsAttachments ?? new StoresBranchsAttachmentsRepository(_context);
        public IStoresProductsAttachmentsRepository StoresProductsAttachments => _storesProductsAttachments = _storesProductsAttachments ?? new StoresProductsAttachmentsRepository(_context);
        public IStoresRepository Stores => _storesRepository = _storesRepository ?? new StoresRepository(_context);
        public IUserCoinsRepository UserCoins => _userCoinsRepository = _userCoinsRepository ?? new UserCoinsRepository(_context);
        public IUserVouchersRepository UserVouchers => _userVouchersRepository = _userVouchersRepository ?? new UserVouchersRepository(_context);
        public IUserSocialStoreRepository UserSocialStore => _userSocialStoreRepository = _userSocialStoreRepository ?? new UserSocialStoreRepository(_context);
        public IUserSocialLoginRepository UserSocialLogin => _userSocialLoginRepository = _userSocialLoginRepository ?? new UserSocialLoginRepository(_context);
        public IUserLogsRepository UserLogs => _userLogsRepository = _userLogsRepository ?? new UserLogsRepository(_context);
        public IUserStoresRepository UserStores => _userStoresRepository = _userStoresRepository ?? new UserStoresRepository(_context);
        public IVouchersRepository Vouchers => _vouchersRepository = _vouchersRepository ?? new VouchersRepository(_context);
        public ISliderHomeRepository sliderHome => _sliderHomeRepository = _sliderHomeRepository ?? new SliderHomeRepository(_context);
        public ILocationTestRepositiry LocationTestRepository => _locationTestRepository = _locationTestRepository ?? new LocationTestRepository(_context);

        /// <summary>
        /// Save changes async
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            int result = await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return result;
        }

        /// <summary>
        /// Save Changes not asunc
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
