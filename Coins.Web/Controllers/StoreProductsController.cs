using AutoMapper;
using Coins.Core;
using Coins.Core.Constants;
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
    public class StoreProductsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StorageService _storage;
        public StoreProductsController(
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreProductVM model, IFormFile MainAttachment, List<IFormFile> Attachments)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<StoreProducts>(model);
                newObj.CreateByUserId = CurrentUser.Id;
                newObj.StoreId = CurrentUser.StoreId.Value;
                //Insert MainAttachment
                {
                    var attachmentResult = await _storage.UploadFile(MainAttachment);
                    if (attachmentResult is not null)
                        newObj.StoreProductMainAttachmentId = attachmentResult.Id;
                }
                await _unitOfWork.StoreProducts.AddAsync(newObj);
                await _unitOfWork.CommitAsync();

                //Insert Attachments
                {
                    var attachmentsResult = await _storage.UploadFiles(Attachments);
                    if (attachmentsResult is not null)
                    {
                        await _unitOfWork.StoresProductsAttachments.AddRangeAttachments(userId: CUserId, storeProductId: newObj.StoreProductId, attachments: attachmentsResult);
                        await _unitOfWork.CommitAsync();
                    }
                }

                ViewData["EditStatus"] = "تمت عملية الاضافة بنجاح";
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _unitOfWork.StoreProducts.GetByIdAsync(id);
            return View(_mapper.Map<StoreProductVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreProductVM model, IFormFile MainAttachment, List<IFormFile> Attachments)
        {
            if (ModelState.IsValid)
            {
                var baseObj = await _unitOfWork.StoreProducts.GetByIdAsync(id);
                PropertyCopy.Copy(model, baseObj);
                baseObj.UpdateAt = DateTime.Now;
                baseObj.UpdateByUserId = CurrentUser.Id;
                //Insert MainAttachment
                {
                    var attachmentResult = await _storage.UploadFile(MainAttachment);
                    if (attachmentResult is not null)
                        baseObj.StoreProductMainAttachmentId = attachmentResult.Id;
                }
                _unitOfWork.StoreProducts.Update(baseObj);
                await _unitOfWork.CommitAsync();

                //Insert Attachments
                {
                    var attachmentsResult = await _storage.UploadFiles(Attachments);
                    if (attachmentsResult is not null)
                    {
                        await _unitOfWork.StoresProductsAttachments.AddRangeAttachments(userId: CUserId, storeProductId: baseObj.StoreProductId, attachments: attachmentsResult);
                        await _unitOfWork.CommitAsync();
                    }
                }

                TempData["EditStatus"] = "تمت عملية التعديل بنجاح";
                return RedirectToAction(nameof(MyStoreController.Index), "MyStore");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.StoreProducts.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.StoreProducts.Remove(item);
                await _unitOfWork.CommitAsync();
            }
            TempData["EditStatus"] = "تمت عملية الحذف بنجاح";
            return RedirectToAction(nameof(MyStoreController.Index), "MyStore");
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = await _unitOfWork.StoreProducts.Filter(
                totalPages: out var totalPages,
                filter: x => x.StoreId == CurrentUser.StoreId && (d.SearchKey == null
                || x.StoreProductNameAr.Contains(d.SearchKey)
                || x.StoreProductNameEn.Contains(d.SearchKey)
                || x.StoreProductPrice.ToString().Equals(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt)).ToListAsync();
          
            var items = rolesList.Select(x => new
            {
                x.StoreProductId,
                x.StoreProductNameAr,
                x.StoreProductNameEn,
                x.StoreProductPrice,
                x.StoreProductDescriptionAr,
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
