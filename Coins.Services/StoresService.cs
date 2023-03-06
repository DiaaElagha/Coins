using AutoMapper;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Converters;
using Coins.Core.Helpers;
using Coins.Core.Helpers.Models;
using Coins.Core.Models.Domins.Attachments;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.DtoAPI.Store;
using Coins.Core.Models.DtoAPI.User;
using Coins.Core.Services;
using Coins.Core.Services.ThiedParty;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.StoresInfo;
using GeoAPI.CoordinateSystems.Transformations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Distance;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Coins.Services
{
    public class StoresService : IStoresService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        private readonly INotificationsService _notificationService;
        public StoresService(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            INotificationsService notificationService,
            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._notificationService = notificationService;
            this.mapper = mapper;
        }

        #region Get
        public async Task<Wrapper<List<object>>> GetStoresMain(Guid userId, StoresParamMainDto model)
        {
            var skip = model.size * (model.page - 1);

            var iQueryable = _unitOfWork.StoreBranchs.Get()
                .Include(x => x.Store)
                .ThenInclude(x => x.StoreCategory)
                .Include(x => x.StoreUserFavoritesList)
                .Where(x => (!x.IsDeleted) && (x.Store.IsPublish) && (x.Store.IsActive))
                .WhereIf((model?.services?.Count() ?? 0) > 0,
                    x => model.services.Intersect(x.StoreBranchsAdvantagesList.Select(x => x.AdvantageId).ToArray()).Any())
                .WhereIf(model.categoryId != null, x => x.Store.StoreCategoryId == model.categoryId)
                .WhereIf(!string.IsNullOrEmpty(model.search), x =>
                    x.BranchNameAr.Contains(model.search) ||
                    x.BranchNameEn.Contains(model.search) ||
                    x.Store.StoreNameAr.Contains(model.search) ||
                    x.Store.StoreNameEn.Contains(model.search))
                .OrderByDescending(v => v.CreateAt)
                .AsQueryable();

            Point pointUser = null;
            if (model.userLatitude.HasValue && model.userLongitude.HasValue && model.distance.HasValue)
            {
                pointUser = ExtensionMethods.CreatePoint(model.userLatitude.Value, model.userLongitude.Value);
                iQueryable = iQueryable
                    .Where(x => x.Location != null && x.BranchLatitudeLocation != null && x.BranchLongitudeLocation != null &&
                        x.Location.Distance(pointUser) <= model.distance)
                    .OrderByDescending(x => x.Location.Distance(pointUser));
            }

            var totalItems = await iQueryable.CountAsync();
            var IQueryableMapping = iQueryable.Select(x => x.ToMainObject(userId, pointUser));

            var listStoreBranchs = await IQueryableMapping
                .Skip(skip)
                .Take(model.size)
                .ToListAsync();

            return new Wrapper<List<object>>
            {
                Data = listStoreBranchs,
                Pagination = new Pagination(model.size, totalItems)
            };
        }

        public async Task<Wrapper<List<object>>> GetStoresMostVisited(StoresParamDto model)
        {
            var skip = model.size * (model.page - 1);

            var listMostVisitedBranchs = await _unitOfWork.UserCoins.Get()
                .GroupBy(x => x.StoreBranchId)
                .Select(x => new { key = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .Skip(skip)
                .Take(model.size)
                .Select(x => x.key)
                .ToListAsync();

            var IQueryableStoreBranchs = _unitOfWork.StoreBranchs.Get()
                .Where(x => (!x.IsDeleted) && (x.Store.IsPublish) && (x.Store.IsActive)
                    && listMostVisitedBranchs.Contains(x.BranchId))
                .Include(v => v.Store)
                .ThenInclude(v => v.StoreCategory)
                .Select(x => x.ToMainObject(null, null));

            var totalItems = await IQueryableStoreBranchs.CountAsync();

            var listStoreBranchs = await IQueryableStoreBranchs.Skip(skip).Take(model.size).ToListAsync();

            return new Wrapper<List<object>>
            {
                Data = listStoreBranchs,
                Pagination = new Pagination(model.size, totalItems)
            };
        }

        public async Task<Wrapper<List<object>>> GetStoresVisitedBefore(Guid userId, StoresParamDto model)
        {
            var skip = model.size * (model.page - 1);

            var IQueryableStoreBranchs = _unitOfWork.UserStores.Get()
                .Where(x => (x.UserId == userId) && (!x.IsDeleted) && (x.LastVisitStoreBranch.Store.IsActive))
                .OrderByDescending(v => v.CreateAt)
                .Include(v => v.LastVisitStoreBranch)
                .ThenInclude(v => v.Store)
                .ThenInclude(v => v.StoreCategory)
                .Select(x => x.LastVisitStoreBranch.ToMainObject(userId, null));

            var totalItems = await IQueryableStoreBranchs.CountAsync();

            var listStoreBranchs = await IQueryableStoreBranchs.Skip(skip).Take(model.size).ToListAsync();

            return new Wrapper<List<object>>
            {
                Data = listStoreBranchs,
                Pagination = new Pagination(model.size, totalItems)
            };
        }

        public async Task<Wrapper<List<StoreCategory>>> GetStoreCategories(int size, int page)
        {
            var skip = size * (page - 1);
            var IQueryableCategories = _unitOfWork.StoreCategory.Get()
                .Where(x => (!x.IsDeleted))
                .OrderByDescending(v => v.CreateAt);

            var totalItems = await IQueryableCategories.CountAsync();
            var listCategories = await IQueryableCategories.Skip(skip).Take(size).ToListAsync();

            return new Wrapper<List<StoreCategory>>
            {
                Data = listCategories,
                Pagination = new Pagination(size, totalItems)
            };
        }

        public async Task<Wrapper<List<StoreProducts>>> GetStoreMenu(int storeId, int size, int page)
        {
            var skip = size * (page - 1);
            var IQueryableStoreProducts = _unitOfWork.StoreProducts.Get()
                .Where(x => (!x.IsDeleted) && (x.StoreId == storeId))
                .OrderByDescending(v => v.CreateAt)
                .Include(v => v.Store)
                .ThenInclude(v => v.StoreCategory);

            var totalItems = await IQueryableStoreProducts.CountAsync();
            var listStoreProducts = await IQueryableStoreProducts.Skip(skip).Take(size).ToListAsync();

            return new Wrapper<List<StoreProducts>>
            {
                Data = listStoreProducts,
                Pagination = new Pagination(size, totalItems)
            };
        }

        public async Task<object> GetStoreById(Guid userId, int storeBranchId, bool addToSearch)
        {
            var storeBranchItem = await _unitOfWork.StoreBranchs.Get()
                .Where(x => x.BranchId == storeBranchId)
                .Include(x => x.StoresAttachmentsList)
                .Include(x => x.StoreBranchsAdvantagesList)
                .ThenInclude(x => x.Advantage)
                .Include(x => x.Store)
                .ThenInclude(x => x.StoreCategory)
                .Include(x => x.Store.SocialTypesStoresList.Where(c => c.IsActive))
                .ThenInclude(x => x.Voucher)
                .Include(x => x.Store.StoreProductsList.OrderByDescending(o => o.CreateAt).Take(5))
                .Include(x => x.Store.VouchersList.Where(c => c.IsActive).OrderByDescending(o => o.CreateAt).Take(5))
                .Include(x => x.UserSocialStoreList.Where(c => c.UserId == userId))
                .Include(x => x.Store.UserStoresList.Where(c => c.UserId == userId))
                .SingleOrDefaultAsync();
            if (storeBranchItem is null)
                throw new NotFoundException(storeBranchId);

            // Add visit to StoreBranch
            if (addToSearch)
            {
                storeBranchItem.NumOfSearch = storeBranchItem.NumOfSearch + 1;
                _unitOfWork.StoreBranchs.Update(storeBranchItem);
                await _unitOfWork.CommitAsync();
            }

            var checkIfFirstVisit = storeBranchItem.Store.UserStoresList.FirstOrDefault();
            storeBranchItem.ReferrralCode = checkIfFirstVisit?.ReferrralCode ?? null;
            // To create First Visit Voucher
            if (checkIfFirstVisit is null)
            {
                if (storeBranchItem.Store.FirstTimeVoucherId.HasValue)
                {
                    var userVoucherFirstVisit = new UserVouchers
                    {
                        VoucherId = storeBranchItem.Store.FirstTimeVoucherId.Value,
                        UserId = userId,
                        CreateByUserId = userId
                    };
                    await _unitOfWork.UserVouchers.AddAsync(userVoucherFirstVisit);
                    await _unitOfWork.CommitAsync();
                }
            }

            return storeBranchItem.ToFullObject(userId);
        }

        public async Task<Wrapper<List<object>>> GetFirstTimeVouchers(int size, int page)
        {
            var skip = size * (page - 1);

            var IQueryableVouchers = _unitOfWork.Vouchers.Get()
                .Where(x => (x.VoucherType == VoucherType.FirstTime) && (x.Store.IsActiveFirstTimeVoucher))
                .OrderByDescending(v => v.CreateAt)
                .Include(v => v.Store)
                .ThenInclude(v => v.StoreCategory);

            var totalItems = await IQueryableVouchers.CountAsync();

            var listVouchers = await IQueryableVouchers
                .Skip(skip)
                .Take(size)
                .Select(x => x.ToMainObject(null))
                .ToListAsync();

            return new Wrapper<List<object>>
            {
                Data = listVouchers,
                Pagination = new Pagination(size, totalItems)
            };
        }

        public async Task<object> GetAdvantages(int storeBranchId, int size, int page)
        {
            var skip = size * (page - 1);
            var IQueryableStoreBranchs = _unitOfWork.StoreBranchsAdvantages.Get()
                .Include(x => x.Advantage)
                .Where(x => x.StoreBranchId == storeBranchId)
                .OrderByDescending(v => v.CreateAt)
                .Select(x => x.Advantage.ToAdvantageObject());
            var totalItems = await IQueryableStoreBranchs.CountAsync();
            var listStoreBranchs = await IQueryableStoreBranchs
                .Skip(skip)
                .Take(size)
                .ToListAsync();
            return new Wrapper<List<object>>
            {
                Data = listStoreBranchs,
                Pagination = new Pagination(size, totalItems)
            };
        }

        public async Task<object> GetStoresCoins(Guid userId, int size, int page)
        {
            var skip = size * (page - 1);
            var query = _unitOfWork.UserStores.Get()
                .Include(x => x.Store)
                .Where(x => x.UserId == userId)
                .OrderByDescending(v => v.LastVisitAt)
                .Select(x => x.ToMainObject(null));
            var totalItems = await query.CountAsync();
            var listData = await query
                .Skip(skip)
                .Take(size)
                .ToListAsync();
            return new Wrapper<List<object>>
            {
                Data = listData,
                Pagination = new Pagination(size, totalItems)
            };
        }
        #endregion

        #region UnlockVoucher

        public async Task<bool> UnlockVoucher(Guid userId, int voucherId)
        {
            var voucherItem = await _unitOfWork.Vouchers.Get()
                .Include(x => x.Store)
                .SingleOrDefaultAsync(x => x.VoucherId == voucherId);

            if (voucherItem.VoucherType != VoucherType.Normal)
                throw new BadRequestException("Voucher type must be (VoucherType.Normal)");

            var userStoreBranch = await _unitOfWork.UserStores.Get()
                .Where(x => x.UserId == userId && x.StoreId == voucherItem.StoreId)
                .Include(x => x.UserStore)
                .SingleOrDefaultAsync();
            if (userStoreBranch is null)
                throw new BadRequestException("Client not exist in table (UserStores)");

            var sumCoinsUser = await _unitOfWork.UserCoins.GetSumCoins(userId: userId, store: voucherItem.Store);

            if (sumCoinsUser < voucherItem.NumOfCoins)
            {
                // SendNotification 
                // --> NotificationsConst.FailedUnlockVoucher;
                await _notificationService.Send(userStoreBranch.UserStore.FcmToken, NotificationsConst.FailedUnlockVoucher());
            }

            await CreateUserVouchers(userId, voucherId);

            // Deduct X Coins from Wallet user

            int numCoinsDeducted = voucherItem.NumOfCoins; // 500
            int numCoinsCollected = 0;
            var userCoinsItems = await _unitOfWork.UserCoins.Get().Where(x => x.StoreId == voucherItem.StoreId && x.Remaining > 0).OrderByDescending(x => x.CoinExpiryDate).ToListAsync();
            foreach (var item in userCoinsItems)
            {
                if (numCoinsDeducted <= numCoinsCollected)
                    break;
                numCoinsCollected += item.NumberOfCoinCollected; // 100
                int remainigCalculated = item.NumberOfCoinCollected - numCoinsCollected; // 100 - 0 = 100
                if (remainigCalculated <= 0)
                {
                    item.Remaining = 0;
                }
                else
                {
                    item.Remaining = remainigCalculated;
                }
                _unitOfWork.UserCoins.Update(item);
            }
            await _unitOfWork.CommitAsync();

            await UpdateTotalCoins(userId, voucherItem.Store, userStoreBranch, sumCoinsUser);

            // SendNotification 
            // --> NotificationsConst.SuccessUnlockVoucher;
            await _notificationService.Send(userStoreBranch.UserStore.FcmToken, NotificationsConst.SuccessUnlockVoucher());
            return true;
        }

        private async Task CreateUserVouchers(Guid userId, int voucherId)
        {
            var userVoucher = new UserVouchers
            {
                VoucherId = voucherId,
                UserId = userId,
                CreateByUserId = userId
            };
            await _unitOfWork.UserVouchers.AddAsync(userVoucher);
            await _unitOfWork.CommitAsync();
        }

        #endregion

        #region SetInvoice by cashier
        public async Task<bool> SetInvoice(Guid cashierId, SetInvoiceDto model)
        {
            var clientItem = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == model.UserId);
            if (clientItem is null)
                throw new NotFoundException(model.UserId);
            var cashierItem = await _userManager.Users
                                .Include(x => x.Store)
                                .Include(x => x.StoreBranch)
                                .SingleOrDefaultAsync(x => x.Id == cashierId);
            if (cashierItem?.StoreId is null)
                throw new NotFoundException(cashierId);
            var userStoreBranch = await _unitOfWork.UserStores
                .GetSingleAsync(x => x.UserId == model.UserId && x.StoreId == cashierItem.StoreId);

            var lastVisitStoreAt = DateTimeOffset.Now;

            if (userStoreBranch is null)
                userStoreBranch = await CreateUserStore(model, cashierItem);
            else
            {
                lastVisitStoreAt = userStoreBranch.LastVisitAt.Value;
                await UpdateUserStore(cashierItem, userStoreBranch);
            }

            int numOfCoinCollected = Convert.ToInt16(model.InvoiceValue * cashierItem.Store.DefineCurrentCurrencyToCoins);

            numOfCoinCollected = ExchangeCoins(lastVisitStoreAt, numOfCoinCollected);

            await CreateUserCoins(cashierId, model, cashierItem.Store, numOfCoinCollected, cashierItem.StoreBranchId);

            await UpdateTotalCoins(model.UserId, cashierItem.Store, userStoreBranch);

            // SendNotification 
            // NotificationsConst.ReceivedCoins;
            if (cashierItem.Store.StoreNameAr.IsNotNullOrEmpty() && clientItem.FcmToken.IsNotNullOrEmpty())
            {
                await _notificationService.Send(
                    id: clientItem.FcmToken,
                    NotificationsConst.ReceivedCoins(numCoins: numOfCoinCollected.ToString(),
                    storeName: cashierItem.Store.StoreNameAr));
            }
            return true;
        }

        private async Task UpdateTotalCoins(Guid userId, Stores store, UserStores userStoreBranch, int? totalCoins = null)
        {
            var sumCoins = await _unitOfWork.UserCoins.GetSumCoins(userId: userId, store: store);

            userStoreBranch.TotalCoins = sumCoins;
            _unitOfWork.UserStores.Update(userStoreBranch);
            await _unitOfWork.CommitAsync();
        }

        private async Task UpdateUserStore(ApplicationUser cashierItem, UserStores userStoreBranch)
        {
            if (userStoreBranch is not null)
            {
                userStoreBranch.LastVisitStoreBranchId = cashierItem.StoreBranchId.Value;
                userStoreBranch.NumOfVisitStore = userStoreBranch.NumOfVisitStore + 1;
                userStoreBranch.LastVisitAt = DateTimeOffset.Now;
                _unitOfWork.UserStores.Update(userStoreBranch);
                await _unitOfWork.CommitAsync();
            }
        }

        private async Task CreateUserCoins(Guid userId, SetInvoiceDto model, Stores store, int numOfCoinCollected, int? storeBranchId = null)
        {
            UserCoins userCoins = new UserCoins
            {
                Remaining = numOfCoinCollected,
                NumberOfCoinCollected = numOfCoinCollected,
                StoreBranchId = storeBranchId,
                StoreId = store.StoreId,
                UserId = model.UserId,
                InvoiceValue = model.InvoiceValue,
                InvoiceNumber = model.InvoiceNumber,
                CreateByUserId = userId,
            };
            if (store.ExpiedCoinsAfterDays.HasValue)
                userCoins.CoinExpiryDate = DateTimeOffset.Now.AddDays(store.ExpiedCoinsAfterDays.Value);

            await _unitOfWork.UserCoins.AddAsync(userCoins);
            await _unitOfWork.CommitAsync();
        }

        private static int ExchangeCoins(DateTimeOffset lastVisitStoreAt, int numOfCoinCollected)
        {
            int daysBetweenVisits = Convert.ToInt16(Math.Round((DateTimeOffset.Now - lastVisitStoreAt).TotalDays));

            switch (daysBetweenVisits)
            {
                case 0:
                    break;
                case <= 7:
                    numOfCoinCollected = Convert.ToInt16(numOfCoinCollected * 2);
                    break;
                case > 7 and <= 14:
                    numOfCoinCollected = Convert.ToInt16(numOfCoinCollected * 1.5);
                    break;
                default:
                    break;
            }

            return numOfCoinCollected;
        }

        private async Task<UserStores> CreateUserStore(SetInvoiceDto model, ApplicationUser cashierItem)
        {
            UserStores userStoreBranch = new UserStores
            {
                UserId = model.UserId,
                StoreId = cashierItem.StoreId.Value,
                LastVisitStoreBranchId = cashierItem.StoreBranchId.Value,
                LastVisitAt = DateTimeOffset.Now,
                NumOfVisitStore = 1,
                CreateByUserId = cashierItem.Id,
                ReferrralCode = string.Format("{0}{1}", cashierItem.StoreId.Value, model.UserId.ToString().Replace("-", "").Substring(0, 8))
            };
            await _unitOfWork.UserStores.AddAsync(userStoreBranch);
            await _unitOfWork.CommitAsync();
            return userStoreBranch;
        }
        #endregion

        #region Rateing
        public async Task<double> AddRate(Guid userId, int storeBranchId, double rateValue)
        {
            if (!await _unitOfWork.StoreBranchs.Get().AnyAsync(x => x.BranchId == storeBranchId))
                throw new NotFoundException(storeBranchId);
            var storeRateResult = await CreateStoreRate(userId, storeBranchId, rateValue);
            var avgRateResult = await UpdateAvgStoreBranchRate(storeBranchId);
            return avgRateResult;
        }

        private async Task<double> UpdateAvgStoreBranchRate(int storeBranchId)
        {
            var avgRateValue = await _unitOfWork.StoreRate.Get().Where(x => x.StoreBranchId == storeBranchId).AsNoTracking().AverageAsync(x => x.RateValue);

            var storeBranchItem = await _unitOfWork.StoreBranchs.GetByIdAsync(storeBranchId);
            storeBranchItem.AvgRate = avgRateValue;
            _unitOfWork.StoreBranchs.Update(storeBranchItem);
            await _unitOfWork.CommitAsync();
            return avgRateValue;
        }

        private async Task<StoreRate> CreateStoreRate(Guid userId, int storeBranchId, double rateValue)
        {
            var storeRateResult = await _unitOfWork.StoreRate.GetSingleAsync(x => x.UserId == userId && x.StoreBranchId == storeBranchId);
            if (storeRateResult is null)
            {
                storeRateResult = new StoreRate
                {
                    StoreBranchId = storeBranchId,
                    UserId = userId,
                    CreateByUserId = userId,
                    RateValue = rateValue
                };
                await _unitOfWork.StoreRate.AddAsync(storeRateResult);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                storeRateResult.RateValue = rateValue;
                _unitOfWork.StoreRate.Update(storeRateResult);
                await _unitOfWork.CommitAsync();
            }
            return storeRateResult;
        }

        #endregion

        #region Favorites
        public async Task ChangeFavorite(Guid userId, int storeBranchId)
        {
            if (!await _unitOfWork.StoreBranchs.Get().AnyAsync(x => x.BranchId == storeBranchId))
                throw new NotFoundException(storeBranchId);
            var storeFavoriteResult = await _unitOfWork.StoreFavorites.GetSingleAsync(x => x.UserId == userId && x.StoreBranchId == storeBranchId);
            if (storeFavoriteResult is null)
            {
                storeFavoriteResult = new StoreUserFavorites
                {
                    StoreBranchId = storeBranchId,
                    UserId = userId,
                    CreateByUserId = userId
                };
                await _unitOfWork.StoreFavorites.AddAsync(storeFavoriteResult);
                await _unitOfWork.CommitAsync();
            }
            else
            {
                _unitOfWork.StoreFavorites.Remove(storeFavoriteResult);
                await _unitOfWork.CommitAsync();
            }
        }

        #endregion

        #region Media & friends
        public async Task<bool> SocialMediaActivityCliam(int mediaId, int storeBranchId, Guid userId)
        {
            var storeBranchItem = await _unitOfWork.StoreBranchs.Get()
                .Where(x => x.BranchId == storeBranchId)
                .Include(x => x.Store)
                .SingleOrDefaultAsync();
            if (storeBranchItem is null)
                throw new NotFoundException(storeBranchId);
            var socialTypeStore = await _unitOfWork.SocialTypesStores
                .Get().SingleOrDefaultAsync(x => x.StoreBranchId == storeBranchItem.BranchId &&
                    x.StoreId == storeBranchItem.StoreId && ((int)x.SocialType) == mediaId);
            if (socialTypeStore is null)
                throw new BadRequestException("Store branch not defind this social type");
            var checkSocialStoreItem = _unitOfWork.UserSocialStore.Any(x =>
                ((int)x.SocialType) == mediaId && x.StoreBranchId == storeBranchItem.BranchId && x.UserId == userId);
            if (checkSocialStoreItem)
                throw new BadRequestException("Client already active in this social type");

            var userStoreBranch = await _unitOfWork.UserStores
                 .GetSingleAsync(x => x.UserId == userId && x.StoreId == storeBranchItem.Store.StoreId);

            int numOfCoinCollected = socialTypeStore?.RewardNumberOfCoins ?? 0;
            if (numOfCoinCollected == 0)
                throw new BadRequestException("There is no coins defind in this social type to reward");

            await CreateUserSocialStore(mediaId, userId, storeBranchItem);

            await CreateUserCoins(userId, new SetInvoiceDto { UserId = userId }, storeBranchItem.Store, numOfCoinCollected);

            await UpdateTotalCoins(userId, storeBranchItem.Store, userStoreBranch);

            // SendNotification 
            // NotificationsConst.ReceivedCoins;
            var userItem = await _userManager.FindByIdAsync(userId.ToString());
            await _notificationService.Send(userItem.FcmToken, NotificationsConst.ReceivedCoins(numCoins: numOfCoinCollected.ToString(), storeName: storeBranchItem.Store.StoreNameAr));

            return true;
        }

        public async Task<bool> ReferralFriend(Guid userId, int storeId)
        {
            var storeBranchItem = await _unitOfWork.StoreBranchs.Get().Where(x => x.BranchId == storeId).Include(x => x.Store).SingleOrDefaultAsync();

            var checkReferralFriend = false;

            if (checkReferralFriend)
            {
                return false;
            }
            else
            {
                var userStoreBranch = await _unitOfWork.UserStores
                 .GetSingleAsync(x => x.UserId == userId && x.StoreId == storeBranchItem.Store.StoreId);

                int numOfCoinCollected = storeBranchItem?.Store?.DefineNumOfReferralCustomerCoins ?? 0;
                if (numOfCoinCollected == 0)
                    return false;

                await CreateUserCoins(userId, new SetInvoiceDto { UserId = userId }, storeBranchItem.Store, numOfCoinCollected);

                await UpdateTotalCoins(userId, storeBranchItem.Store, userStoreBranch);

                // SendNotification 
                // NotificationsConst.ReceivedCoins;
                var userItem = await _userManager.FindByIdAsync(userId.ToString());
                await _notificationService.Send(userItem.FcmToken, NotificationsConst.ReceivedCoins(numCoins: numOfCoinCollected.ToString(), storeName: storeBranchItem.Store.StoreNameAr));
            }
            return await Task.FromResult(true);
        }

        private async Task CreateUserSocialStore(int mediaId, Guid userId, StoreBranchs storeBranchItem)
        {
            var userSocialStore = new UserSocialStore
            {
                UserId = userId,
                StoreBranchId = storeBranchItem.BranchId,
                SocialType = (SocialType)mediaId,
                CreateByUserId = userId
            };
            await _unitOfWork.UserSocialStore.AddAsync(userSocialStore);
            await _unitOfWork.CommitAsync();
        }
        #endregion

        #region CRUD Store
        public async Task<Stores> CreateStore(Stores stores, string userName, string password)
        {
            await _unitOfWork.Stores.AddAsync(stores);
            await _unitOfWork.CommitAsync();

            {
                // CREATE NEW LOGIN STORE USER
                var appUser = new ApplicationUser
                {
                    UserName = userName,
                    FullName = stores.StoreNameAr,
                    IsActive = stores.IsActive,
                    Role = UsersRoles.StoreAdmin,
                    StoreId = stores.StoreId
                };
                var resultCreateAppUser = await _userManager.CreateAsync(appUser, password);

                // CREATE NEW DEFULT MAIN Branch STORE
                var mainBranch = new StoreBranchs
                {
                    StoreId = stores.StoreId,
                    IsMainBranch = true,
                    BranchNameAr = stores.StoreNameAr,
                    BranchNameEn = stores.StoreNameEn,
                    CreateByUserId = stores.CreateByUserId
                };
                await _unitOfWork.StoreBranchs.AddAsync(mainBranch);
                await _unitOfWork.CommitAsync();

            }
            return stores;
        }
        #endregion

        #region Ads stores
        public async Task<object> GetStoreAds(StoresParamAdsDto model)
        {
            var userPoint = ExtensionMethods.CreatePoint(model.userLatitude, model.userLongitude);
            var IQueryableStoreBranchs = _unitOfWork.StoreBranchs.Get()
                .Where(x => (!x.IsDeleted) && (x.Store.IsPublish) && (x.Store.IsActive) &&
                    (x.Store.StoreCategoryId == model.categoryId))
                .Where(x => x.Location != null && x.BranchLatitudeLocation != null && x.BranchLongitudeLocation != null &&
                    x.Location.Distance(userPoint) <= model.distance)
                .OrderByDescending(x => x.Location.Distance(userPoint))
                .Include(v => v.Store)
                .ThenInclude(v => v.StoreCategory);

            var count = await IQueryableStoreBranchs.CountAsync(); // 1st round-trip
            var index = new Random().Next(count);

            var storeBranch = await IQueryableStoreBranchs.Skip(index).FirstOrDefaultAsync();

            return storeBranch.ToMainObject(null, userPoint);
        }

        #endregion

        #region Check
        public object CheckStoreData(int storeId)
        {
            var storeItem = _unitOfWork.Stores.Get()
                .Select(x => new
                {
                    HomePageIsValid =
                        !(x.LogoImageId == null ||
                        x.StoreNameAr == null ||
                        x.StoreDescriptionAr == null ||
                        x.StoreCategoryId == null ||
                        x.StorePriceTypeId == null),
                    BranchsPageIsValid =
                        !(x.StoreProductsList.Count() == 0 ||
                        x.StoreBranchsList.Any(c =>
                            c.BranchNameAr == null ||
                            c.BranchDescriptionAr == null ||
                            c.StoreBranchsAdvantagesList.Count() == 0 ||
                            c.BranchLatitudeLocation == null ||
                            c.BranchLongitudeLocation == null ||
                            c.BranchMainAttachmentId == null ||
                            c.StoresAttachmentsList.Count() == 0)),
                    PointsRewardsPageIsValid =
                        !(x.DefineCurrentCurrencyToCoins == 0 ||
                        (x.IsActiveExpiedCoins ? (x.ExpiedCoinsAfterDays == null) : false) ||
                        x.SocialTypesStoresList.Where(c => c.SocialType == SocialType.Facebook)
                            .Any(c => c.IsActive ? (string.IsNullOrEmpty(c.UrlLink) && c.RewardNumberOfCoins == null) : false) ||
                        x.SocialTypesStoresList.Where(c => c.SocialType == SocialType.Instagram)
                            .Any(c => c.IsActive ? (string.IsNullOrEmpty(c.UrlLink) && c.RewardNumberOfCoins == null) : false))
                })
                .FirstOrDefault();
            return storeItem;
        }

        #endregion

        #region Home Banner Slider
        public async Task<object> GetSliderHome()
        {
            return await _unitOfWork.sliderHome.Get()
                .OrderByDescending(x => x.CreateAt)
                .Take(10)
                .Include(x => x.StoreBranch)
                .Select(x => x.ToSliderHomeObject())
                .ToListAsync();
        }
        #endregion
    }
}
