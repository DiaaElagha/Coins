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
    public class StoreBranchsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StorageService _storage;

        public StoreBranchsController(
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
            ViewData["AdvantagesList"] = new SelectList(await _unitOfWork.Advantages.GetAllAsync()
                , nameof(Advantages.AdvantageId), nameof(Advantages.AdvantageTitleAr));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreBranchVM model, IFormFile MainAttachment, List<IFormFile> Attachments)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<StoreBranchs>(model.branchVM);
                newObj.CreateByUserId = CurrentUser.Id;
                newObj.StoreId = CurrentUser.StoreId.Value;
                //Insert MainAttachment
                {
                    var attachmentResult = await _storage.UploadFile(MainAttachment);
                    if (attachmentResult is not null)
                        newObj.BranchMainAttachmentId = attachmentResult.Id;
                }
                if (newObj.BranchLatitudeLocation != null && newObj.BranchLongitudeLocation != null)
                {
                    newObj.Location = ExtensionMethods.CreatePoint(newObj.BranchLatitudeLocation.Value, newObj.BranchLongitudeLocation.Value);
                }
                await _unitOfWork.StoreBranchs.AddAsync(newObj);

                if (newObj.IsMainBranch)
                {
                    if (_unitOfWork.StoreBranchs.Any(x => x.StoreId == CurrentUser.StoreId.Value && x.IsMainBranch))
                    {
                        var itemsMainBranchs = await _unitOfWork.StoreBranchs.Get().Where(x => x.StoreId == CurrentUser.StoreId.Value && x.IsMainBranch).ToListAsync();
                        itemsMainBranchs.ForEach(x => { x.IsMainBranch = false; });
                        _unitOfWork.StoreBranchs.UpdateRange(itemsMainBranchs);
                    }
                }
                await _unitOfWork.CommitAsync();

                if (model.AdvantagesList is not null)
                {
                    await UpdateAdvantages(newObj.BranchId, model.AdvantagesList);
                }
                //Insert Attachments
                {
                    var attachmentsResult = await _storage.UploadFiles(Attachments);
                    if (attachmentsResult is not null)
                    {
                        await _unitOfWork.StoresBranchsAttachments.AddRangeAttachments(userId: CUserId, storeBranchId: newObj.BranchId, attachments: attachmentsResult);
                        await _unitOfWork.CommitAsync();
                    }
                }
                TempData["EditStatus"] = "تمت عملية الاضافة بنجاح";
                return RedirectToAction(nameof(this.Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["AdvantagesList"] = new SelectList(await _unitOfWork.Advantages.GetAllAsync()
             , nameof(Advantages.AdvantageId), nameof(Advantages.AdvantageTitleAr));

            var entity = await _unitOfWork.StoreBranchs.GetByIdAsync(id);
            var branchObjVM = _mapper.Map<StoreBranchObjVM>(entity);
            var soreBranchVM = new StoreBranchVM
            {
                AdvantagesList = _unitOfWork.StoreBranchsAdvantages.Find(x => x.StoreBranchId == id).Select(x => x.AdvantageId).ToArray(),
                branchVM = branchObjVM
            };
            return View(soreBranchVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreBranchVM model, IFormFile MainAttachment, List<IFormFile> Attachments)
        {
            if (ModelState.IsValid)
            {
                var baseObj = await _unitOfWork.StoreBranchs.GetByIdAsync(id);
                PropertyCopy.Copy(model.branchVM, baseObj);
                baseObj.UpdateAt = DateTime.Now;
                baseObj.UpdateByUserId = CurrentUser.Id;

                if (baseObj.BranchLatitudeLocation != null && baseObj.BranchLongitudeLocation != null)
                {
                    baseObj.Location =  ExtensionMethods.CreatePoint(baseObj.BranchLatitudeLocation.Value, baseObj.BranchLongitudeLocation.Value);
                }

                //Insert MainAttachment
                {
                    var attachmentResult = await _storage.UploadFile(MainAttachment);
                    if (attachmentResult is not null)
                        baseObj.BranchMainAttachmentId = attachmentResult.Id;
                }
                _unitOfWork.StoreBranchs.Update(baseObj);
                if (baseObj.IsMainBranch)
                {
                    if (_unitOfWork.StoreBranchs.Any(x => x.StoreId == CurrentUser.StoreId.Value && x.IsMainBranch && x.BranchId != id))
                    {
                        var itemsMainBranchs = await _unitOfWork.StoreBranchs.Get().Where(x => x.StoreId == CurrentUser.StoreId.Value && x.IsMainBranch && x.BranchId != id).ToListAsync();
                        itemsMainBranchs.ForEach(x => { x.IsMainBranch = false; });
                        _unitOfWork.StoreBranchs.UpdateRange(itemsMainBranchs);
                    }
                }
                await _unitOfWork.CommitAsync();

                if (model.AdvantagesList is not null)
                {
                    await UpdateAdvantages(baseObj.BranchId, model.AdvantagesList);
                }
                //Insert Attachments
                {
                    var attachmentsResult = await _storage.UploadFiles(Attachments);
                    if (attachmentsResult is not null)
                    {
                        await _unitOfWork.StoresBranchsAttachments.AddRangeAttachments(userId: CUserId, storeBranchId: baseObj.BranchId, attachments: attachmentsResult);
                        await _unitOfWork.CommitAsync();
                    }
                }
                TempData["EditStatus"] = "تمت عملية التعديل بنجاح";
                return RedirectToAction(nameof(this.Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.StoreBranchs.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.StoreBranchs.Remove(item);
                await _unitOfWork.CommitAsync();
            }
            TempData["EditStatus"] = "تمت عملية الحذف بنجاح";
            return RedirectToAction(nameof(this.Index));
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = _unitOfWork.StoreBranchs.Filter(
                totalPages: out var totalPages,
                filter: x => x.StoreId == CurrentUser.StoreId &&
                    (d.SearchKey == null
                    || x.BranchNameAr.Contains(d.SearchKey)
                    || x.BranchNameEn.Contains(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt));

            var dataItems = await rolesList.Skip(d.Start).Take(d.Length).ToListAsync();
            var items = dataItems.Select(x => new
            {
                x.BranchId,
                x.BranchNameAr,
                x.BranchNameEn,
                x.IsMainBranch,
                x.NumOfSearch,
                x.AvgRate,
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

        private async Task UpdateAdvantages(int storeBranchId, int[] AdvantagesList)
        {

            var listOldRoles = _unitOfWork.StoreBranchsAdvantages.Find(x => x.StoreBranchId == storeBranchId).ToList();

            var resultListAdding = AdvantagesList.Where(x => !listOldRoles.Any(p => p.AdvantageId == x)).ToList();
            var resultListRemoving = listOldRoles.Where(x => !AdvantagesList.Any(p => p == x.AdvantageId)).ToList();

            #region RemovedItems
            if (resultListRemoving.Count() != 0)
            {
                _unitOfWork.StoreBranchsAdvantages.RemoveRange(resultListRemoving);
                await _unitOfWork.CommitAsync();
            }
            #endregion

            #region AddedItems
            if (resultListAdding.Count() != 0)
            {
                List<StoreBranchsAdvantages> Items = new List<StoreBranchsAdvantages>();
                foreach (var item in resultListAdding)
                {
                    Items.Add(new StoreBranchsAdvantages { AdvantageId = item, CreateByUserId = CUserId, StoreBranchId = storeBranchId });
                }
                await _unitOfWork.StoreBranchsAdvantages.AddRangeAsync(Items);
                await _unitOfWork.CommitAsync();
            }
            #endregion
        }

    }
}
