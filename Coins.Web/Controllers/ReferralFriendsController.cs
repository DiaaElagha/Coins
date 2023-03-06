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
    public class ReferralFriendsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StorageService _storage;

        public ReferralFriendsController(
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
            var storeItem = await _unitOfWork.Stores.Get().Include(x => x.ReferralFriendVoucher).SingleOrDefaultAsync(x => x.StoreId == CurrentUser.StoreId.Value);
            if (storeItem is null)
                return NotFound();

            var svVM = new ReferralFriendsVM();
            svVM.IsActiveReferralFriend = storeItem.IsActiveReferralFriend;
            svVM.DefineNumOfReferralCustomerCoins = storeItem.DefineNumOfReferralCustomerCoins;

            svVM.voucherFriendReward = new VoucherFriendReward(VoucherType.FriendReward);
            if (storeItem.ReferralFriendVoucherId is null)
            {
                storeItem.ReferralFriendVoucher = new Voucher
                {
                    VoucherType = VoucherType.FriendReward,
                    VoucherTerms = GeneralConstant.VoucherFriendRewardTerms,
                    StoreId = CurrentUser.StoreId,
                    CreateByUserId = CurrentUser.Id
                };
                _unitOfWork.Vouchers.Update(storeItem.ReferralFriendVoucher);
                await _unitOfWork.CommitAsync();

                storeItem.ReferralFriendVoucherId = storeItem.ReferralFriendVoucher.VoucherId;
                _unitOfWork.Stores.Update(storeItem);
                await _unitOfWork.CommitAsync();
            }
            svVM.voucherFriendReward.VoucherTerms = storeItem.ReferralFriendVoucher.VoucherTerms;
            svVM.voucherFriendReward.VoucherNameAr = storeItem.ReferralFriendVoucher.VoucherNameAr;
            svVM.voucherFriendReward.VoucherNameEn = storeItem.ReferralFriendVoucher.VoucherNameEn;
            svVM.voucherFriendReward.VoucherDescrptionAr = storeItem.ReferralFriendVoucher.VoucherDescrptionAr;
            svVM.voucherFriendReward.VoucherDescrptionEn = storeItem.ReferralFriendVoucher.VoucherDescrptionEn;
            svVM.voucherFriendReward.VoucherDiscountValue = storeItem.ReferralFriendVoucher.VoucherDiscountValue ?? 0;
            return View(svVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReferralFriendsVM model)
        {
            if (ModelState.IsValid)
            {
                var storeItem = await _unitOfWork.Stores.Get().Include(x => x.ReferralFriendVoucher).SingleOrDefaultAsync(x => x.StoreId == CurrentUser.StoreId.Value);
                storeItem.DefineNumOfReferralCustomerCoins = model.DefineNumOfReferralCustomerCoins;
                storeItem.IsActiveReferralFriend = model.IsActiveReferralFriend;

                storeItem.UpdateAt = DateTime.Now;
                storeItem.UpdateByUserId = CurrentUser.Id;
                _unitOfWork.Stores.Update(storeItem);

                var ReferralFriendVoucher = storeItem.ReferralFriendVoucher;
                ReferralFriendVoucher.VoucherNameAr = model.voucherFriendReward.VoucherNameAr;
                ReferralFriendVoucher.VoucherNameEn = model.voucherFriendReward.VoucherNameEn;
                ReferralFriendVoucher.VoucherDescrptionAr = model.voucherFriendReward.VoucherDescrptionAr;
                ReferralFriendVoucher.VoucherDescrptionEn = model.voucherFriendReward.VoucherDescrptionEn;
                ReferralFriendVoucher.VoucherTerms = model.voucherFriendReward.VoucherTerms;
                ReferralFriendVoucher.VoucherDiscountValue = model.voucherFriendReward.VoucherDiscountValue;
                ReferralFriendVoucher.UpdateAt = DateTime.Now;
                ReferralFriendVoucher.UpdateByUserId = CurrentUser.Id;

                //Insert VoucherMainAttachment
                {
                    var attachmentResult = await _storage.UploadFile(model.voucherFriendReward.VoucherMainAttachmentId);
                    if (attachmentResult is not null)
                        ReferralFriendVoucher.VoucherMainAttachmentId = attachmentResult.Id;
                }

                _unitOfWork.Vouchers.Update(ReferralFriendVoucher);

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

    }
}
