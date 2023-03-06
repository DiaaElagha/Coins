using AutoMapper;
using Coins.Core;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Services;
using Coins.Web.Helper;
using Coins.Web.Models;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
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
    public class AdvantagesController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StorageService _storage;

        public AdvantagesController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            StorageService storage,
            IUnitOfWork unitOfWork) : base(configuration, userManager, mapper)
        {
            _unitOfWork = unitOfWork;
            _storage = storage;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvantagesVM model, IFormFile ImageIcon)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Advantages>(model);
                newObj.CreateByUserId =  CurrentUser.Id;
                //Insert ImageIcon
                {
                    var attachmentResult = await _storage.UploadFile(ImageIcon);
                    if (attachmentResult is not null)
                        newObj.IconImageId = attachmentResult.Id;
                }
                await _unitOfWork.Advantages.AddAsync(newObj);
                await _unitOfWork.CommitAsync();
                return Content(ShowMessage.AddSuccessResult(), "application/json");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _unitOfWork.Advantages.GetByIdAsync(id);
            return View(_mapper.Map<AdvantagesVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdvantagesVM model, IFormFile ImageIcon)
        {
            if (ModelState.IsValid)
            {
                var baseObj = await _unitOfWork.Advantages.GetByIdAsync(id);
                PropertyCopy.Copy(model, baseObj);
                //Insert ImageIcon
                {
                    var attachmentResult = await _storage.UploadFile(ImageIcon);
                    if (attachmentResult is not null)
                        baseObj.IconImageId = attachmentResult.Id;
                }
                baseObj.UpdateAt = DateTime.Now;
                baseObj.UpdateByUserId = CurrentUser.Id;
                _unitOfWork.Advantages.Update(baseObj);
                await _unitOfWork.CommitAsync();
                return Content(ShowMessage.EditSuccessResult(), "application/json");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.Advantages.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.Advantages.Remove(item);
                await _unitOfWork.CommitAsync();
            }
            return Content(ShowMessage.DeleteSuccessResult(), "application/json");
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = await _unitOfWork.Advantages.Filter(
                totalPages: out var totalPages,
                filter: x => (d.SearchKey == null
                || x.AdvantageTitleAr.Contains(d.SearchKey)
                || x.AdvantageTitleAr.Contains(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt)).ToListAsync();

            var items = rolesList.Select(x => new
            {
                x.AdvantageId,
                x.AdvantageTitleAr,
                x.AdvantageTitleEn,
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
