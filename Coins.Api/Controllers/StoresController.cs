using AutoMapper;
using Coins.Core;
using Coins.Core.Constants;
using Coins.Core.Models.DtoAPI.Store;
using Coins.Core.Services;
using Coins.Entities.Domins.Auth;
using Coins.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Coins.Api.Controllers
{
    public class StoresController : BaseController
    {
        private readonly IStoresService _storesService;
        private readonly StorageService _storageService;
        public StoresController(
            IStoresService storesService,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            StorageService storage) : base(configuration, userManager, httpContextAccessor, mapper)
        {
            _storesService = storesService;
            _storageService = storage;
        }

        [HttpGet("/StoresMain")]
        public async Task<IActionResult> GetStoresMain([FromQuery] StoresParamMainDto model)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetStoresMain(GetCurrentUserId(), model));

        [HttpGet("/Stores/{storeBranchId}")]
        public async Task<IActionResult> GetStoreById(int storeBranchId, bool fromSearch = false)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetStoreById(GetCurrentUserId(), storeBranchId, fromSearch));

        [HttpGet("/Stores/VisitedBefore")]
        public async Task<IActionResult> GetStoresVisitedBefore([FromQuery] StoresParamDto model)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetStoresVisitedBefore(GetCurrentUserId(), model));

        [HttpGet("/Stores/MostVisited")]
        public async Task<IActionResult> GetStoresMostVisited([FromQuery] StoresParamDto model)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetStoresMostVisited(model));

        [HttpGet("/Stores/FT-Vouchers")]
        public async Task<IActionResult> GetFirstTimeVouchers(int size = 15, int page = 1)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetFirstTimeVouchers(size, page));

        [HttpGet("/Stores/GetAdvantages/{storeBranchId}")]
        public async Task<IActionResult> GetAdvantages(int storeBranchId, int size = 15, int page = 1)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetAdvantages(storeBranchId, size, page));

        [HttpGet("/Stores/Categories")]
        public async Task<IActionResult> GetCategories(int size = 15, int page = 1)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetStoreCategories(size: size, page: page));

        [HttpGet("/Stores/Menu/{storeId}")]
        public async Task<IActionResult> GetStoreMenu(int storeId, int size = 15, int page = 1)
            => GetResponse(ResponseMessages.READ, true, await _storesService.GetStoreMenu(storeId: storeId, size: size, page: page));

        [HttpPost("/Stores/Invoice")]
        public async Task<IActionResult> SetInvoice([FromBody] SetInvoiceDto model)
            => GetResponse(ResponseMessages.Operation, true, await _storesService.SetInvoice(GetCurrentUserId(), model));

        [HttpPost("/Stores/AddRate")]
        public async Task<IActionResult> AddRate([FromBody] AddRateDto model)
            => GetResponse(ResponseMessages.Operation, true,
                await _storesService.AddRate(GetCurrentUserId(), model.storeBranchId, model.rateValue));

        [HttpPost("/Stores/ChangeFavorite/{storeBranchId}")]
        public async Task<IActionResult> Favorite(int storeBranchId)
        {
            await _storesService.ChangeFavorite(GetCurrentUserId(), storeBranchId);
            return GetResponse(ResponseMessages.Operation, true);
        }

        [HttpGet("/Stores/BannerSlider")]
        public async Task<IActionResult> HomeBannerSlider()
            => GetResponse(ResponseMessages.Operation, true, await _storesService.GetSliderHome());

        [HttpGet("/File/{id}")]
        public async Task<IActionResult> GetFile(string id)
        {
            var attachmentItem = await _storageService.GetFile(id);
            if (attachmentItem is null)
                throw new NotFoundException(id);
            byte[] bytes = attachmentItem.Data;
            return new FileContentResult(bytes, attachmentItem.MimeType)
            {
                FileDownloadName = attachmentItem.Name
            };
        }

    }
}
