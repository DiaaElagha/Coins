using AutoMapper;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.StoresInfo;
using Coins.Services;
using Coins.Web.Helper;
using Coins.Web.Models;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = UsersRoles.StoreAdmin)]
    public class StoreVouchersController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StorageService _storage;

        public StoreVouchersController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            StorageService storage) : base(configuration, userManager, mapper)
        {
            _unitOfWork = unitOfWork;
            _storage = storage;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var storeItem = await _unitOfWork.Stores.Get().Include(x => x.FirstTimeVoucher).SingleOrDefaultAsync(x => x.StoreId == CurrentUser.StoreId.Value);
            if (storeItem is null)
                return NotFound();

            var svVM = new StoreVouchersVM();
            svVM.IsActiveVoucherDiscount = storeItem.IsActiveVoucherDiscount;
            svVM.IsActiveMaxCoinsToSpentVoucherDiscount = storeItem.IsActiveMaxCoinsToSpentVoucherDiscount;
            svVM.DefineMaxCoinsToSpentVoucherDiscount = storeItem.DefineMaxCoinsToSpentVoucherDiscount;
            svVM.IsActiveMinCoinsRedeemVoucherDiscount = storeItem.IsActiveMinCoinsRedeemVoucherDiscount;
            svVM.DefineMinCoinsRedeemVoucherDiscount = storeItem.DefineMinCoinsRedeemVoucherDiscount;
            svVM.NumberRedeemedCoinsForEveryCurrency = storeItem.NumberRedeemedCoinsForEveryCurrency;
            svVM.IsActiveVoucherDiscountExpiredAfterDay = storeItem.IsActiveVoucherDiscountExpiredAfterDay;
            svVM.VoucherDiscountExpiredAfterDay = storeItem.VoucherDiscountExpiredAfterDay;

            svVM.IsActiveFirstTimeVoucher = storeItem.IsActiveFirstTimeVoucher;
            svVM.VoucherFirstTime = new VoucherFirstTime(VoucherType.FirstTime);
            if (storeItem.FirstTimeVoucherId is null)
            {
                storeItem.FirstTimeVoucher = new Voucher
                {
                    VoucherType = VoucherType.FirstTime,
                    VoucherTerms = GeneralConstant.VoucherFirstTimeTerms,
                    StoreId = CurrentUser.StoreId,
                    CreateByUserId = CurrentUser.Id
                };
                _unitOfWork.Vouchers.Update(storeItem.FirstTimeVoucher);
                await _unitOfWork.CommitAsync();

                storeItem.FirstTimeVoucherId = storeItem.FirstTimeVoucher.VoucherId;
                _unitOfWork.Stores.Update(storeItem);
                await _unitOfWork.CommitAsync();
            }
            svVM.VoucherFirstTime.VoucherTerms = storeItem.FirstTimeVoucher.VoucherTerms;
            svVM.VoucherFirstTime.VoucherNameAr = storeItem.FirstTimeVoucher.VoucherNameAr;
            svVM.VoucherFirstTime.VoucherNameEn = storeItem.FirstTimeVoucher.VoucherNameEn;
            svVM.VoucherFirstTime.VoucherDescrptionAr = storeItem.FirstTimeVoucher.VoucherDescrptionAr;
            svVM.VoucherFirstTime.VoucherDescrptionEn = storeItem.FirstTimeVoucher.VoucherDescrptionEn;
            return View(svVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(StoreVouchersVM model)
        {
            if (ModelState.IsValid)
            {
                var storeItem = await _unitOfWork.Stores.Get().Include(x => x.FirstTimeVoucher).SingleOrDefaultAsync(x => x.StoreId == CurrentUser.StoreId.Value);
                storeItem.DefineMaxCoinsToSpentVoucherDiscount = model.DefineMaxCoinsToSpentVoucherDiscount;
                storeItem.DefineMinCoinsRedeemVoucherDiscount = model.DefineMinCoinsRedeemVoucherDiscount;
                storeItem.IsActiveFirstTimeVoucher = model.IsActiveFirstTimeVoucher;
                storeItem.IsActiveMaxCoinsToSpentVoucherDiscount = model.IsActiveMaxCoinsToSpentVoucherDiscount;
                storeItem.IsActiveMinCoinsRedeemVoucherDiscount = model.IsActiveMinCoinsRedeemVoucherDiscount;
                storeItem.IsActiveVoucherDiscount = model.IsActiveVoucherDiscount;
                storeItem.IsActiveVoucherDiscountExpiredAfterDay = model.IsActiveVoucherDiscountExpiredAfterDay;
                storeItem.NumberRedeemedCoinsForEveryCurrency = model.NumberRedeemedCoinsForEveryCurrency;
                storeItem.VoucherDiscountExpiredAfterDay = model.VoucherDiscountExpiredAfterDay;

                storeItem.UpdateAt = DateTime.Now;
                storeItem.UpdateByUserId = CurrentUser.Id;
                _unitOfWork.Stores.Update(storeItem);

                var FirstTimeVoucher = storeItem.FirstTimeVoucher;
                FirstTimeVoucher.VoucherNameAr = model.VoucherFirstTime.VoucherNameAr;
                FirstTimeVoucher.VoucherNameEn = model.VoucherFirstTime.VoucherNameEn;
                FirstTimeVoucher.VoucherDescrptionAr = model.VoucherFirstTime.VoucherDescrptionAr;
                FirstTimeVoucher.VoucherDescrptionEn = model.VoucherFirstTime.VoucherDescrptionEn;
                FirstTimeVoucher.VoucherTerms = model.VoucherFirstTime.VoucherTerms;
                FirstTimeVoucher.UpdateAt = DateTime.Now;
                FirstTimeVoucher.UpdateByUserId = CurrentUser.Id;

                //Insert VoucherMainAttachment
                {
                    var attachmentResult = await _storage.UploadFile(model.VoucherFirstTime.VoucherMainAttachmentId);
                    if (attachmentResult is not null)
                        FirstTimeVoucher.VoucherMainAttachmentId = attachmentResult.Id;
                }

                _unitOfWork.Vouchers.Update(FirstTimeVoucher);

                await _unitOfWork.CommitAsync();

                TempData["EditStatus"] = "تمت عملية التعديل بنجاح";
                return View(model);
            }
            ViewData["CategoriesList"] = new SelectList(await _unitOfWork.StoreCategory.GetAllAsync()
               , nameof(StoreCategory.StoreCategoryId), nameof(StoreCategory.StoreCategoryNameAr));
            ViewData["PriceTypesList"] = new SelectList(await _unitOfWork.StorePriceType.GetAllAsync()
               , nameof(StorePriceType.StorePriceTypeId), nameof(StorePriceType.StorePriceTypeAr));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var storeProducts = _unitOfWork.StoreProducts.Find(x => x.StoreId == CurrentUser.StoreId).ToList();
            ViewData["StoreProductsList"] = new SelectList(storeProducts
                , nameof(StoreProducts.StoreProductId), nameof(StoreProducts.StoreProductNameAr));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoucherVM model, IFormFile MainAttachment)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Voucher>(model);
                newObj.CreateByUserId = CurrentUser.Id;
                newObj.StoreId = CurrentUser.StoreId.Value;
                //Insert MainAttachment
                {
                    var attachmentResult = await _storage.UploadFile(MainAttachment);
                    if (attachmentResult is not null)
                        newObj.VoucherMainAttachmentId = attachmentResult.Id;
                }
                await _unitOfWork.Vouchers.AddAsync(newObj);
                await _unitOfWork.CommitAsync();
                TempData["EditStatus"] = "تمت عملية الاضافة بنجاح";
                return RedirectToAction(nameof(this.Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var storeProducts = _unitOfWork.StoreProducts.Find(x => x.StoreId == CurrentUser.StoreId).ToList();
            ViewData["StoreProductsList"] = new SelectList(storeProducts
                , nameof(StoreProducts.StoreProductId), nameof(StoreProducts.StoreProductNameAr));

            var entity = await _unitOfWork.Vouchers.GetByIdAsync(id);
            return View(_mapper.Map<VoucherVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VoucherVM model, IFormFile MainAttachment)
        {
            if (ModelState.IsValid)
            {
                var baseObj = await _unitOfWork.Vouchers.GetByIdAsync(id);
                PropertyCopy.Copy(model, baseObj);
                baseObj.UpdateAt = DateTime.Now;
                baseObj.UpdateByUserId = CurrentUser.Id;
                //Insert MainAttachment
                {
                    var attachmentResult = await _storage.UploadFile(MainAttachment);
                    if (attachmentResult is not null)
                        baseObj.VoucherMainAttachmentId = attachmentResult.Id;
                }
                _unitOfWork.Vouchers.Update(baseObj);
                await _unitOfWork.CommitAsync();

                TempData["EditStatus"] = "تمت عملية التعديل بنجاح";
                return RedirectToAction(nameof(this.Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.Vouchers.GetByIdAsync(id);
            if (item != null)
            {
                var firstTimeVoucher = await _unitOfWork.Stores.GetFirstAsync(x => x.FirstTimeVoucherId == item.VoucherId);
                if (firstTimeVoucher is not null)
                {
                    firstTimeVoucher.FirstTimeVoucherId = null;
                    _unitOfWork.Stores.Update(firstTimeVoucher);
                    await _unitOfWork.CommitAsync();
                }

                _unitOfWork.Vouchers.Remove(item);
                await _unitOfWork.CommitAsync();
            }
            return Content(ShowMessage.DeleteSuccessResult(), "application/json");
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = await _unitOfWork.Vouchers.Filter(
                totalPages: out var totalPages,
                filter: x => (x.VoucherType == VoucherType.Normal) && (d.SearchKey == null
                || x.VoucherNameAr.Contains(d.SearchKey)
                || x.VoucherNameEn.Contains(d.SearchKey)
                || x.NumOfCoins.ToString().Equals(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt)).ToListAsync();

            var items = rolesList.Select(x => new
            {
                x.VoucherId,
                x.VoucherNameAr,
                x.VoucherNameEn,
                x.NumOfCoins,
                x.IsActive,
                x.VoucherExpiredAfterDay,
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


    }
}
