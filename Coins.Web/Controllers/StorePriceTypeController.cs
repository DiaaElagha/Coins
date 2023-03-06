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
    public class StorePriceTypeController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public StorePriceTypeController(
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
        public async Task<IActionResult> Create(StorePriceTypeVM model)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<StorePriceType>(model);
                newObj.CreateByUserId =  CurrentUser.Id;
                await _unitOfWork.StorePriceType.AddAsync(newObj);
                await _unitOfWork.CommitAsync();
                return Content(ShowMessage.AddSuccessResult(), "application/json");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _unitOfWork.StorePriceType.GetByIdAsync(id);
            return View(_mapper.Map<StorePriceTypeVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StorePriceTypeVM model)
        {
            if (ModelState.IsValid)
            {
                var baseObj = await _unitOfWork.StorePriceType.GetByIdAsync(id);
                PropertyCopy.Copy(model, baseObj);
                baseObj.UpdateAt = DateTime.Now;
                baseObj.UpdateByUserId = CurrentUser.Id;
                _unitOfWork.StorePriceType.Update(baseObj);
                await _unitOfWork.CommitAsync();
                return Content(ShowMessage.EditSuccessResult(), "application/json");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.StorePriceType.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.StorePriceType.Remove(item);
                await _unitOfWork.CommitAsync();
            }
            return Content(ShowMessage.DeleteSuccessResult(), "application/json");
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = await _unitOfWork.StorePriceType.Filter(
                totalPages: out var totalPages,
                filter: x => (d.SearchKey == null
                || x.StorePriceTypeAr.Contains(d.SearchKey)
                || x.StorePriceTypeEn.Contains(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt)).ToListAsync();
            
            var items = rolesList.Select(x => new
            {
                x.StorePriceTypeId,
                x.StorePriceTypeAr,
                x.StorePriceTypeEn,
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
