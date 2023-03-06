using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Coins.Web.Controllers;
using Coins.Core;
using Microsoft.Extensions.Configuration;
using Coins.Entities.Domins.Auth;
using Coins.Core.Models.Domins.Home;
using Coins.Core.Constants;
using Coins.Web.Models.ViewModels;
using Coins.Core.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Coins.Core.Models.Domins.StoresInfo;
using Microsoft.AspNetCore.Http;
using Coins.Services;
using Coins.Web.Helper;

namespace Coins.Web.Controllers
{
    [Authorize(Roles = UsersRoles.StoreAdmin)]
    public class MyStoreController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StorageService _storage;
        public MyStoreController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,

            StorageService storage,
            IUnitOfWork unitOfWork) : base(configuration, userManager, mapper)
        {
            _storage = storage;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CategoriesList"] = new SelectList(await _unitOfWork.StoreCategory.GetAllAsync()
               , nameof(StoreCategory.StoreCategoryId), nameof(StoreCategory.StoreCategoryNameAr));
            ViewData["PriceTypesList"] = new SelectList(await _unitOfWork.StorePriceType.GetAllAsync()
               , nameof(StorePriceType.StorePriceTypeId), nameof(StorePriceType.StorePriceTypeAr));

            var entity = await _unitOfWork.Stores.GetByIdAsync(CurrentUser.StoreId.Value);
            return View(_mapper.Map<MyStoreEditVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(MyStoreEditVM model, IFormFile ImageLogo)
        {
            if (!ModelState.IsValid)
                return View(model);

            var baseObj = await _unitOfWork.Stores.GetByIdAsync(CurrentUser.StoreId.Value);
            PropertyCopy.Copy(model, baseObj);
            baseObj.UpdateAt = DateTime.Now;
            baseObj.UpdateByUserId = CurrentUser.Id;
            //Insert ImageLogo

            var attachmentResult = await _storage.UploadFile(ImageLogo);
            if (attachmentResult is not null)
                baseObj.LogoImageId = attachmentResult.Id;

            _unitOfWork.Stores.Update(baseObj);
            await _unitOfWork.CommitAsync();

            var resultAppUser = await _userManager.Users.FirstOrDefaultAsync(x => x.StoreId == baseObj.StoreId);
            resultAppUser.IsActive = baseObj.IsActive;
            resultAppUser.FullName = baseObj.StoreNameAr;
            await _userManager.UpdateAsync(resultAppUser);

            ViewData["EditStatus"] = "تمت عملية التعديل بنجاح";
            ViewData["CategoriesList"] = new SelectList(await _unitOfWork.StoreCategory.GetAllAsync()
               , nameof(StoreCategory.StoreCategoryId), nameof(StoreCategory.StoreCategoryNameAr));
            ViewData["PriceTypesList"] = new SelectList(await _unitOfWork.StorePriceType.GetAllAsync()
               , nameof(StorePriceType.StorePriceTypeId), nameof(StorePriceType.StorePriceTypeAr));
            return View(model);
        }

        [HttpPost]
        public async Task<string> PublishStore()
        {
            try
            {
                var storeItem = await _unitOfWork.Stores.GetByIdAsync(CurrentUser.StoreId.Value);
                storeItem.IsPublish = true;
                _unitOfWork.Stores.Update(storeItem);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                return "error";
            }
            return "done";
        }

    }
}
