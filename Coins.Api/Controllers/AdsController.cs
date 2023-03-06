using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Coins.Api.Model;
using Coins.Api.Services;
using Coins.Api.Utilities;
using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Core.Helpers.Models;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.DtoAPI.Store;
using Coins.Core.Models.DtoAPI.User;
using Coins.Core.Services;
using Coins.Entities.Domins.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Coins.Api.Controllers
{
    public class AdsController : BaseController
    {
        private readonly IStoresService _storesService;
        public AdsController(
            IStoresService storesService,

            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(configuration, userManager, httpContextAccessor, mapper)
        {
            _storesService = storesService;
        }

        [HttpGet("/Ads/Banner")]
        public async Task<IActionResult> GetStoreAd(int category, double latitude, double longitude)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetStoreAds(
               new StoresParamAdsDto
               {
                   categoryId = category,
                   distance = 500,
                   userLatitude = latitude,
                   userLongitude = longitude
               }));
    }
}
