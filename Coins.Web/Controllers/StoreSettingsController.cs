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

namespace Coins.Web.Controllers
{
    [Authorize(Roles = UsersRoles.StoreAdmin)]
    public class StoreSettingsController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        public StoreSettingsController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,

            IUnitOfWork unitOfWork) : base(configuration, userManager, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vochersResult = _unitOfWork.Vouchers.Find(x => x.StoreId == CurrentUser.StoreId).ToList();
            ViewData["VouchersList"] = new SelectList(vochersResult
               , nameof(Voucher.VoucherId), nameof(Voucher.VoucherNameAr));

            var entity = await _unitOfWork.Stores.GetByIdAsync(CurrentUser.StoreId.Value);
            return View(_mapper.Map<StoreSettingsVM>(entity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(StoreSettingsVM model)
        {
            if (model == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var baseObj = await _unitOfWork.Stores.GetByIdAsync(CurrentUser.StoreId.Value);
                    PropertyCopy.Copy(model, baseObj);
                    baseObj.UpdateAt = DateTime.Now;
                    baseObj.UpdateByUserId = CurrentUser.Id;
                    _unitOfWork.Stores.Update(baseObj);
                    await _unitOfWork.CommitAsync();
                    ViewData["EditStatus"] = "تمت عملية التعديل بنجاح";
                    return View(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewData["EditStatus"] = "فشلت عملية التعديل";
                }
            }
            return View(model);
        }


    }
}
