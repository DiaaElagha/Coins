using AutoMapper;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.Home;
using Coins.Entities.Domins.Auth;
using Coins.Services;
using Coins.Web.Helper;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Coins.Web.Controllers
{
    [Authorize(Roles = UsersRoles.Admin)]
    public class SliderHomeController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StorageService _storage;

        public SliderHomeController(
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
        public async Task<IActionResult> Create()
        {
            ViewData["StoreBranchsList"] = new SelectList(await _unitOfWork.StoreBranchs.Get()
                .Include(x => x.Store).Where(x => x.Store.IsPublish)
                .Select(x => new { StoreBranchId = x.BranchId, Name = x.BranchNameAr })
                .ToListAsync(), "StoreBranchId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderHomeVM model, IFormFile ImageBaner)
        {
            if (!ModelState.IsValid)
                return View(model);

            var newObj = _mapper.Map<SliderHome>(model);
            newObj.CreateByUserId = CurrentUser.Id;

            var attachmentResult = await _storage.UploadFile(ImageBaner);
            if (attachmentResult is not null)
                newObj.AttachmentId = attachmentResult.Id;

            await _unitOfWork.sliderHome.AddAsync(newObj);
            await _unitOfWork.CommitAsync();
            return Content(ShowMessage.AddSuccessResult(), MediaTypeNames.Application.Json);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["StoreBranchsList"] = new SelectList(await _unitOfWork.StoreBranchs.Get()
                .Include(x => x.Store).Where(x => x.Store.IsPublish)
                .Select(x => new { StoreBranchId = x.BranchId, Name = x.BranchNameAr })
                .ToListAsync(), "StoreBranchId", "Name");
            var entity = await _unitOfWork.sliderHome.GetByIdAsync(id);
            return View(_mapper.Map<SliderHomeVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderHomeVM model, IFormFile ImageBaner)
        {
            if (!ModelState.IsValid)
                return View(model);

            var baseObj = await _unitOfWork.sliderHome.GetByIdAsync(id);
            PropertyCopy.Copy(model, baseObj);

            var attachmentResult = await _storage.UploadFile(ImageBaner);
            if (attachmentResult is not null)
                baseObj.AttachmentId = attachmentResult.Id;

            baseObj.UpdateAt = DateTime.Now;
            baseObj.UpdateByUserId = CurrentUser.Id;
            _unitOfWork.sliderHome.Update(baseObj);
            await _unitOfWork.CommitAsync();
            return Content(ShowMessage.EditSuccessResult(), "application/json");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.sliderHome.GetByIdAsync(id);
            if (item is null)
                return Content(ShowMessage.FailedResult(), "application/json");
            _unitOfWork.sliderHome.Remove(item);
            await _unitOfWork.CommitAsync();
            return Content(ShowMessage.DeleteSuccessResult(), "application/json");
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = await _unitOfWork.sliderHome.Filter(
                totalPages: out var totalPages,
                filter: x => (d.SearchKey == null
                || x.TitleAr.Contains(d.SearchKey)
                || x.TitleEn.Contains(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt),
                include: x => x.Include(p => p.StoreBranch)).ToListAsync();

            var items = rolesList.Select(x => new
            {
                x.Id,
                x.TitleAr,
                x.TitleEn,
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
