using AutoMapper;
using Coins.Core.Constants;
using Coins.Entities.Domins.Auth;
using Coins.Services;
using Coins.Web.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Coins.Web.Controllers
{
    [Authorize(Roles = UsersRoles.Admin)]
    public class StorageController : BaseController
    {
        private readonly StorageService _storageService;

        public StorageController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            StorageService storage) : base(configuration, userManager, mapper)
        {
            _storageService = storage;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var attachmentItem = await _storageService.GetFile(id);
            if (attachmentItem is null)
                return Ok("");
            return Ok(Convert.ToBase64String(attachmentItem.Data));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _storageService.RemoveFile(id);
            return Content(ShowMessage.AddSuccessResult(), "application/json");
        }

    }
}
