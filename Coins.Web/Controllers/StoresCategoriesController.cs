using AutoMapper;
using Coins.Core;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Web.Helper;
using Coins.Web.Models;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class StoresCategoriesController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public StoresCategoriesController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,

            IUnitOfWork unitOfWork) : base(configuration, userManager, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StoreCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<StoreCategory>(model);
                newObj.CreateByUserId =  CurrentUser.Id;
                await _unitOfWork.StoreCategory.AddAsync(newObj);
                await _unitOfWork.CommitAsync();
                return Content(ShowMessage.AddSuccessResult(), "application/json");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _unitOfWork.StoreCategory.GetByIdAsync(id);
            return View(_mapper.Map<StoreCategoryVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoreCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var baseObj = await _unitOfWork.StoreCategory.GetByIdAsync(id);
                PropertyCopy.Copy(model, baseObj);
                baseObj.UpdateAt = DateTime.Now;
                baseObj.UpdateByUserId = CurrentUser.Id;
                _unitOfWork.StoreCategory.Update(baseObj);
                await _unitOfWork.CommitAsync();
                return Content(ShowMessage.EditSuccessResult(), "application/json");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.StoreCategory.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.StoreCategory.Remove(item);
                await _unitOfWork.CommitAsync();
            }
            return Content(ShowMessage.DeleteSuccessResult(), "application/json");
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = await _unitOfWork.StoreCategory.Filter(
                totalPages: out var totalPages,
                filter: x => (d.SearchKey == null
                || x.StoreCategoryNameAr.Contains(d.SearchKey)
                || x.StoreCategoryNameEn.Contains(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt)).ToListAsync();
            
            var items = rolesList.Select(x => new
            {
                x.StoreCategoryId,
                x.StoreCategoryNameAr,
                x.StoreCategoryNameEn,
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
