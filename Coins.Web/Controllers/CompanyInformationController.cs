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

namespace Coins.Web.Controllers
{
    //[Authorize(Roles = "administrator")]
    public class CompanyInformationController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        public CompanyInformationController(
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
            if (!IsExists())
            {
                GeneralSettings information = new GeneralSettings();
                await _unitOfWork.GeneralSettings.AddAsync(information);
                await _unitOfWork.CommitAsync();
            }
            var info = await _unitOfWork.GeneralSettings.GetFirstAsync(x => true);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(GeneralSettings model)
        {
            if (model == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.GeneralSettings.Update(model);
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

        private bool IsExists()
        {
            return _unitOfWork.GeneralSettings.Any(x => true);
        }

    }
}
