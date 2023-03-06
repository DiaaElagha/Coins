using AutoMapper;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.StoresInfo;
using Coins.Web.Helper;
using Coins.Web.Models;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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
    public class CashersController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public CashersController(
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
            var storeBranchs = _unitOfWork.StoreBranchs.Find(x => x.StoreId == CurrentUser.StoreId).ToList();
            ViewData["StoreBranchsList"] = new SelectList(storeBranchs
               , nameof(StoreBranchs.BranchId), nameof(StoreBranchs.BranchNameAr));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CasherVM model)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.Users.AnyAsync(x => x.UserName.Equals(model.CasherUserName)))
                {
                    ModelState.AddModelError("StoreUserName", "User name is already taken.");
                    return View(model);
                }

                var appUser = new ApplicationUser
                {
                    UserName = model.CasherUserName,
                    FullName = model.FullName,
                    Address = model.Address,
                    StoreBranchId = model.StoreBranchId,
                    IsActive = model.IsActive,
                    Gender = model.Gender,
                    ExpiryDate = model.ExpiryDate,
                    Role = UsersRoles.Casher,
                    StoreId = CurrentUser.StoreId,
                };

                var resultCreateAppUser = await _userManager.CreateAsync(appUser, ExtensionMethods.GetRandomString());
                return Content(ShowMessage.AddSuccessResult(), "application/json");
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var storeBranchs = _unitOfWork.StoreBranchs.Find(x => x.StoreId == CurrentUser.StoreId).ToList();
            ViewData["StoreBranchsList"] = new SelectList(storeBranchs
               , nameof(StoreBranchs.BranchId), nameof(StoreBranchs.BranchNameAr));
            var entity = await _userManager.FindByIdAsync(id.ToString());
            return View(_mapper.Map<CasherEditVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CasherEditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var entity = await _userManager.FindByIdAsync(id.ToString());
            entity.FullName = model.FullName;
            entity.Address = model.Address;
            entity.IsActive = model.IsActive;
            entity.Gender = model.Gender;
            entity.ExpiryDate = model.ExpiryDate;
            entity.UpdateAt = DateTimeOffset.Now;
            await _userManager.UpdateAsync(entity);
            return Content(ShowMessage.EditSuccessResult(), "application/json");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.Stores.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.Stores.Remove(item);
                await _unitOfWork.CommitAsync();
            }
            return Content(ShowMessage.DeleteSuccessResult(), "application/json");
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            string jsonString = data.ToString();
            JObject ss = JObject.Parse(jsonString);

            int storeBranchId = (int)ss["storeBranchId"];

            var usersList = _userManager.Users
            .Where(x =>
                x.StoreBranchId == storeBranchId &&
                x.Role.Equals(UsersRoles.Casher) &&
                (d.SearchKey == null
                || x.FullName.Contains(d.SearchKey)
                || x.UserName.Contains(d.SearchKey)))
            .OrderBy(x => x.CreateAt);

            int totalCount = usersList.Count();
            var itemsData = await usersList.Skip(d.Start).Take(d.Length).ToListAsync();

            var items = itemsData.Select(x => new
            {
                x.Id,
                x.FullName,
                x.UserName,
                x.StoreBranchId,
                x.IsActive,
                Gender = x.Gender.ToString(),
                LastLoginTime = x.LastLogin.Year > 1 ? x.LastLogin.ToString("MM/dd/yyyy h:mm tt") : "-",
                ExpiryDate = x?.ExpiryDate?.ToString("MM/dd/yyyy") ?? "-",
                UpdateDate = x?.UpdateAt?.ToString("MM/dd/yyyy") ?? "-",
                InsertDate = x?.CreateAt?.ToString("MM/dd/yyyy") ?? "-",
            }).ToList();
            var result =
               new
               {
                   draw = d.Draw,
                   recordsTotal = totalCount,
                   recordsFiltered = totalCount,
                   data = items
               };
            return Json(result);
        }


    }
}
