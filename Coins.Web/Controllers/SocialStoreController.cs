using AutoMapper;
using Coins.Core;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.StoresInfo;
using Coins.Web.Helper;
using Coins.Web.Models;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Controllers
{
    public class SocialStoreController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public SocialStoreController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,

            IUnitOfWork unitOfWork) : base(configuration, userManager, mapper)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Create()
        {
            GetViewDataModel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialStoreVM model)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<SocialTypesStores>(model);
                newObj.StoreId = CurrentUser.StoreId.Value;
                newObj.CreateByUserId = CurrentUser.Id;
                await _unitOfWork.SocialTypesStores.AddAsync(newObj);
                await _unitOfWork.CommitAsync();
                return Content(ShowMessage.AddSuccessResult(), "application/json");
            }
            return View(model);
        }

        public async Task<IActionResult> Index(int id)
        {
            GetViewDataModel(id);

            var storeItem = await _unitOfWork.Stores.GetByIdAsync(CurrentUser.StoreId.Value);
            if (storeItem is null)
                return NotFound();

            var storeBranchs = await _unitOfWork.StoreBranchs.Get().Where(x => x.StoreId == CurrentUser.StoreId).ToListAsync();
            var storeBranchIds = storeBranchs.Select(x => x.BranchId).ToList();
            var mainStoreBranch = storeBranchs.FirstOrDefault(x => x.IsMainBranch);
            if (mainStoreBranch is null)
                return NotFound();

            List<SocialType> listTypes = ExtensionMethods.GetEnumAsListValues<SocialType>();

            foreach (var itemType in listTypes)
            {
                if (itemType == SocialType.GoogleMapRate)
                {
                    foreach (var itemBranch in storeBranchs)
                    {
                        if (!_unitOfWork.SocialTypesStores.Any(x => x.SocialType.Equals(itemType) && x.StoreId == storeItem.StoreId && x.StoreBranchId == itemBranch.BranchId))
                        {
                            var newObj = new SocialTypesStores
                            {
                                StoreId = storeItem.StoreId,
                                StoreBranchId = itemBranch.BranchId,
                                SocialType = itemType,
                                CreateByUserId = CurrentUser.Id,
                            };
                            await _unitOfWork.SocialTypesStores.AddAsync(newObj);
                        }
                    }
                }
                else
                {
                    if (!_unitOfWork.SocialTypesStores.Any(x => x.SocialType.Equals(itemType) && x.StoreId == storeItem.StoreId && x.StoreBranchId == mainStoreBranch.BranchId))
                    {
                        var newObj = new SocialTypesStores
                        {
                            StoreId = storeItem.StoreId,
                            StoreBranchId = mainStoreBranch.BranchId,
                            SocialType = itemType,
                            CreateByUserId = CurrentUser.Id,
                        };
                        await _unitOfWork.SocialTypesStores.AddAsync(newObj);
                    }
                }

            }
            await _unitOfWork.CommitAsync();

            var socialTypesList = _unitOfWork.SocialTypesStores.Find(x => x.StoreId == CurrentUser.StoreId).ToList();
            var socialStoresVM = _mapper.Map<List<SocialStoreVM>>(socialTypesList.Where(x => !x.SocialType.Equals(SocialType.GoogleMapRate)).ToList());
            var socialStoresGMVM = _mapper.Map<List<SocialStoreGMVM>>(socialTypesList.Where(x => x.SocialType.Equals(SocialType.GoogleMapRate)).ToList());
            EarnCoinsVM earnCoinsVM = new EarnCoinsVM
            {
                SocialStores = socialStoresVM,
                SocialStoresGoogleMap = socialStoresGMVM,
                DefineCurrentCurrencyToCoins = storeItem.DefineCurrentCurrencyToCoins,
                ExpiedCoinsAfterDays = storeItem.ExpiedCoinsAfterDays,
                IsActiveExpiedCoins = storeItem.IsActiveExpiedCoins,
                IsActiveGoogleMapRate = storeItem.IsActiveGoogleMapRate
            };

            return View(earnCoinsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EarnCoinsVM model)
        {
            GetViewDataModel();
            if (ModelState.IsValid)
            {
                var storeItem = await _unitOfWork.Stores.GetByIdAsync(CurrentUser.StoreId.Value);
                if (storeItem is null)
                    return NotFound();
                storeItem.DefineCurrentCurrencyToCoins = model.DefineCurrentCurrencyToCoins;
                storeItem.ExpiedCoinsAfterDays = model.ExpiedCoinsAfterDays;
                storeItem.IsActiveExpiedCoins = model.IsActiveExpiedCoins;
                storeItem.IsActiveGoogleMapRate = model.IsActiveGoogleMapRate;
                _unitOfWork.Stores.Update(storeItem);

                foreach (var itemType in model.SocialStores)
                {
                    var socialStoreItem = await _unitOfWork.SocialTypesStores.GetFirstAsync(x => x.SocialType == itemType.SocialType && x.StoreId == CurrentUser.StoreId);
                    if (socialStoreItem is null)
                        continue;
                    socialStoreItem.UrlLink = itemType.UrlLink;
                    socialStoreItem.RewardNumberOfCoins = itemType.RewardNumberOfCoins;
                    socialStoreItem.IsActive = itemType.IsActive;
                    socialStoreItem.UpdateByUserId = CUserId;
                    socialStoreItem.UpdateAt = DateTime.Now;
                    _unitOfWork.SocialTypesStores.Update(socialStoreItem);
                }

                foreach (var itemType in model.SocialStoresGoogleMap)
                {
                    var socialStoreItem = await _unitOfWork.SocialTypesStores.GetFirstAsync(x => x.SocialType == itemType.SocialType && x.StoreId == CurrentUser.StoreId && x.StoreBranchId == itemType.StoreBranchId);
                    if (socialStoreItem is null)
                        continue;
                    socialStoreItem.StoreBranchId = itemType.StoreBranchId;
                    socialStoreItem.RewardNumberOfCoins = itemType.RewardNumberOfCoins;
                    socialStoreItem.UpdateByUserId = CUserId;
                    socialStoreItem.UpdateAt = DateTime.Now;
                    _unitOfWork.SocialTypesStores.Update(socialStoreItem);
                }
                await _unitOfWork.CommitAsync();

                TempData["EditStatus"] = "تمت عملية التعديل بنجاح";
                return View(model);
            }
            return View(model);
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = await _unitOfWork.SocialTypesStores.Filter(
                totalPages: out var totalPages,
                filter: x => true,
                orderBy: x => x.OrderByDescending(p => p.CreateAt),
                include: x => x.Include(x => x.Voucher)).ToListAsync();

            var items = rolesList.Select(x => new
            {
                SocialTypeId = (int)x.SocialType,
                SocialTypeName = x.SocialType.GetDescription(),
                x.VoucherId,
                VoucherName = x?.Voucher?.VoucherNameAr ?? "-",
                x.RewardNumberOfCoins,
                x.UrlLink,
                RewardTypeName = x.RewardType.GetDescription(),
                x.IsActive,
                InsertDate = x.CreateAt.Value.ToString("MM/dd/yyyy"),
            }).Skip(d.Start).Take(d.Length).ToList();
            var result =
               new
               {
                   draw = d.Draw,
                   recordsTotal = totalPages,
                   recordsFiltered = totalPages,
                   data = items
               };
            return Json(result);
        }

        private void GetViewDataModel(int? id = null)
        {
            ViewData["StoreBranchs"] = new SelectList(_unitOfWork.StoreBranchs.Find(x => x.StoreId == CurrentUser.StoreId).ToList(), nameof(StoreBranchs.BranchId), nameof(StoreBranchs.BranchNameAr));
        }
    }
}
