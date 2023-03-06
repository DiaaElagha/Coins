using AutoMapper;
using Coins.Core.Constants;
using Coins.Core.Helpers;
using Coins.Core.Services;
using Coins.Entities.Domins.Auth;
using Coins.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coins.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected UserManager<ApplicationUser> _userManager;
        protected readonly IConfiguration _configuration;
        protected IMapper _mapper;

        protected Guid CUserId = new Guid();
        protected ApplicationUser CurrentUser = null;

        public BaseController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (User.Identity.IsAuthenticated)
            {
                base.OnActionExecuting(context);
                var userId = new Guid(_userManager.GetUserId(HttpContext.User));
                var objCurrentUser = HttpContext.Session.GetObject<ApplicationUser>("CurrentUser");
                if (objCurrentUser == null)
                {
                    SetValuesInSession(userId);
                }
                else if (objCurrentUser != null)
                {
                    CurrentUser = objCurrentUser;
                    CUserId = objCurrentUser.Id;
                }
                if (userId != CurrentUser.Id)
                {
                    SetValuesInSession(userId);
                }

                if (CurrentUser.StoreId != null && CurrentUser.Role.Equals(UsersRoles.StoreAdmin.ToString()))
                {
                    //var storesService = context.HttpContext.RequestServices.GetRequiredService<IStoresService>();
                    //var ss = storesService.CheckStoreData(CurrentUser.StoreId.Value);

                }
                // Set data in ViewBag
                ViewBag.UserId = CurrentUser?.Id.ToString() ?? "";
                ViewBag.Role = CurrentUser?.Role ?? "";
                ViewBag.UserName = CurrentUser?.UserName ?? "";
                ViewBag.FullName = CurrentUser?.FullName ?? "";
                ViewBag.Email = CurrentUser?.Email ?? "";
                ViewBag.PhoneNumber = CurrentUser?.PhoneNumber ?? "";
            }

            void SetValuesInSession(Guid userId)
            {
                CUserId = userId;
                CurrentUser = _userManager.FindByIdAsync(CUserId.ToString()).Result;
                HttpContext.Session.SetObject("CurrentUser", CurrentUser);
            }
        }

        [NonAction]
        public IActionResult NotFound()
        {
            return Redirect($"~/Error?code={(int)HttpStatusCode.NotFound}");
        }

        [NonAction]
        public IActionResult NotFound(string value)
        {
            return Redirect($"~/Error?code={(int)HttpStatusCode.NotFound}&message={value}");
        }

        [NonAction]
        public IActionResult BadRequest()
        {
            return Redirect($"~/Error?code={(int)HttpStatusCode.BadRequest}");
        }

        [NonAction]
        public IActionResult BadRequest(string value)
        {
            return Redirect($"~/Error?code={(int)HttpStatusCode.BadRequest}&message={value}");
        }

        [NonAction]
        public IActionResult Forbid()
        {
            return Redirect($"~/Error?code={(int)HttpStatusCode.Forbidden}");
        }

        public string AbsoluteUri()
        {
            var absoluteUri = "";
            try
            {
                absoluteUri = string.Concat(Request.Scheme, "://", Request.Host.ToUriComponent(), Request.PathBase.ToUriComponent());
            }
            catch
            {
                return absoluteUri;
            }
            return absoluteUri;
        }

    }
}
