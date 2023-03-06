using AutoMapper;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Entities.Domins.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Controllers
{
    public class LookUpsController : BaseController
    {
        public LookUpsController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(configuration, userManager, httpContextAccessor, mapper)
        {

        }

        [HttpGet]
        public ActionResult VouchersStatus() => Ok(ExtensionMethods.GetEnumAsDictionary<VoucherStatus>());
        [HttpGet]
        public ActionResult VouchersType() => Ok(ExtensionMethods.GetEnumAsDictionary<VoucherType>());
        [HttpGet]
        public ActionResult Genders() => Ok(ExtensionMethods.GetEnumAsDictionary<Gender>());
        [HttpGet]
        public ActionResult SocialsTypes() => Ok(ExtensionMethods.GetEnumAsDictionary<SocialType>());
        [HttpGet]
        public ActionResult StoreRegisterStatus() => Ok(ExtensionMethods.GetEnumAsDictionary<StoreRegisterStatus>());
        [HttpGet]
        public ActionResult RewardTypes() => Ok(ExtensionMethods.GetEnumAsDictionary<RewardType>());

    }
}
