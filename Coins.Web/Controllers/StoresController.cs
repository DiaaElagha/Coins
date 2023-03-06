using AutoMapper;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Services;
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
    [Authorize(Roles = UsersRoles.Admin)]
    public class StoresController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StorageService _storage;
        private readonly IStoresService _storesService;

        public StoresController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            StorageService storage,
            IUnitOfWork unitOfWork,
            IStoresService storesService) : base(configuration, userManager, mapper)
        {
            _unitOfWork = unitOfWork;
            _storage = storage;
            _storesService = storesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoriesList"] = new SelectList(await _unitOfWork.StoreCategory.GetAllAsync()
                , nameof(StoreCategory.StoreCategoryId), nameof(StoreCategory.StoreCategoryNameAr));
            ViewData["PriceTypesList"] = new SelectList(await _unitOfWork.StorePriceType.GetAllAsync()
                , nameof(StorePriceType.StorePriceTypeId), nameof(StorePriceType.StorePriceTypeAr));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreVM model, IFormFile ImageLogo)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.Users.AnyAsync(x => x.UserName.Equals(model.StoreUserName)))
                {
                    ModelState.AddModelError("StoreUserName", "User name is already taken.");
                    return View();
                }
                var newObj = _mapper.Map<Stores>(model);
                newObj.CreateByUserId = CurrentUser.Id;
                //Insert ImageLogo
                {
                    var attachmentResult = await _storage.UploadFile(ImageLogo);
                    if (attachmentResult is not null)
                        newObj.LogoImageId = attachmentResult.Id;
                }

                await _storesService.CreateStore(newObj, model.StoreUserName, model.StorePassword);

                return Content(ShowMessage.AddSuccessResult(), "application/json");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["CategoriesList"] = new SelectList(await _unitOfWork.StoreCategory.GetAllAsync()
               , nameof(StoreCategory.StoreCategoryId), nameof(StoreCategory.StoreCategoryNameAr));
            ViewData["PriceTypesList"] = new SelectList(await _unitOfWork.StorePriceType.GetAllAsync()
               , nameof(StorePriceType.StorePriceTypeId), nameof(StorePriceType.StorePriceTypeAr));
            var entity = await _unitOfWork.Stores.GetByIdAsync(id);
            return View(_mapper.Map<StoreEditVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreEditVM model, IFormFile ImageLogo)
        {
            if (ModelState.IsValid)
            {
                var baseObj = await _unitOfWork.Stores.GetByIdAsync(id);
                PropertyCopy.Copy(model, baseObj);
                baseObj.UpdateAt = DateTime.Now;
                baseObj.UpdateByUserId = CurrentUser.Id;
                //Insert ImageLogo
                {
                    var attachmentResult = await _storage.UploadFile(ImageLogo);
                    if (attachmentResult is not null)
                        baseObj.LogoImageId = attachmentResult.Id;
                }
                _unitOfWork.Stores.Update(baseObj);
                await _unitOfWork.CommitAsync();

                {
                    var resultAppUser = await _userManager.Users.FirstOrDefaultAsync(x => x.StoreId == baseObj.StoreId);
                    resultAppUser.IsActive = baseObj.IsActive;
                    resultAppUser.FullName = baseObj.StoreNameAr;
                    if (!model.Password.IsNullOrEmpty())
                    {
                        resultAppUser.PasswordHash = ExtensionMethods.HashPassword(model.Password.Trim());
                    }
                    await _userManager.UpdateAsync(resultAppUser);
                }

                return Content(ShowMessage.EditSuccessResult(), "application/json");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.Stores.GetByIdAsync(id);
            _unitOfWork.Stores.Remove(item);
            await _unitOfWork.CommitAsync();
            return Content(ShowMessage.DeleteSuccessResult(), "application/json");
        }

        [HttpPost]
        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = _unitOfWork.Stores.Filter(
                totalPages: out int totalPages,
                filter: x => (d.SearchKey == null
                    || x.StoreNameAr.Contains(d.SearchKey)
                    || x.StoreNameEn.Contains(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt),
                include: x => x.Include(x => x.StoreCategory));

            var dataItems = await rolesList.Skip(d.Start).Take(d.Length).ToListAsync();

            var items = dataItems.Select(x => new
            {
                x.StoreId,
                x.StoreNameAr,
                x.StoreNameEn,
                x.IsActive,
                StoreCategoryName = x.StoreCategory?.StoreCategoryNameAr ?? "-",
                InsertDate = x.CreateAt.Value.ToString("MM/dd/yyyy"),
            }).ToList();
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
