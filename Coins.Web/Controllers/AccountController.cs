using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Entities.Domins.Auth;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coins.Web.Controllers
{
    [Authorize]
    [Route("[Controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IUnitOfWork _unitOfWork;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("/Login")]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                object userRole = User?.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value ?? "";
                if (userRole.Equals(UsersRoles.Admin))
                    return Redirect("/Stores/Index");
                else if (userRole.Equals(UsersRoles.StoreAdmin))
                    return Redirect("/MyStore/Index");
                else
                    return Redirect("/");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("/Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName.Equals(model.Username));
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            if (!user.IsActive)
            {
                ModelState.AddModelError(string.Empty, "User is inactive.");
                return View(model);
            }
            if (user.ExpiryDate.HasValue)
            {
                if (user.ExpiryDate < DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "User is expired.");
                    return View(model);
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                await _signInManager.SignInWithClaimsAsync(user,
                    new AuthenticationProperties { IsPersistent = model.RememberMe, ExpiresUtc = DateTimeOffset.Now.AddYears(1) }, claims);

                if (returnUrl != null && !returnUrl.ToLower().StartsWith("/home"))
                    return Redirect(returnUrl);
                else if (user.Role == UsersRoles.Admin)
                    return Redirect("/Stores/Index");
                else
                    return Redirect("/MyStore/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpGet("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet("/AccessDenied")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

    }
}