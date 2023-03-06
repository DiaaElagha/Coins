using AutoMapper;
using Coins.Api.Services;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.Domins.Test;
using Coins.Core.Models.DtoAPI.Test;
using Coins.Core.Services;
using Coins.Entities.Domins.Auth;
using Microsoft.AspNetCore.Authorization;
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
    public class TestController : BaseController
    {
        private readonly IAuthService _authService;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ITestService _locationTestService;

        public TestController(
            IAuthService authService,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ITestService locationTestService) : base(configuration, userManager, httpContextAccessor, mapper)
        {
            _authService = authService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _locationTestService = locationTestService;
        }


        [HttpPost]
        [AllowAnonymous]
        // Validate Verification Code
        public async Task<IActionResult> AddTest(LocationTestDTO dto)
        {
            LocationTest test = _mapper.Map<LocationTest>(dto);
            test = await _locationTestService.AddLocatinoTest(test);


            dto = _mapper.Map<LocationTest, LocationTestDTO>(test);
            return Ok(dto);
        }

        [HttpGet]
        [AllowAnonymous]
        // Validate Verification Code
        public async Task<IActionResult> GetTest(int id)
        {
            var test = await _locationTestService.GetLocationTest(id);

            return Ok(_mapper.Map<LocationTest, LocationTestDTO>(test));
        }

        [HttpGet("all")]
        [AllowAnonymous]
        // Validate Verification Code
        public async Task<IActionResult> GetAllTest()
        {
            var test = await _locationTestService.GetAllLocationtest();

            return Ok(_mapper.Map<IEnumerable<LocationTest>, IEnumerable<LocationTestDTO>>(test));
        }

        [HttpGet("nearby")]
        [AllowAnonymous]
        // Validate Verification Code
        public IActionResult GetNearby(double x, double y)
        {
            var test = _locationTestService.GetNearbyLocationTest(x, y, 100);

            return Ok(_mapper.Map<IEnumerable<LocationTest>, IEnumerable<LocationTestDTO>>(test));
        }
    }
}
