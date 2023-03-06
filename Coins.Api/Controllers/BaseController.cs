using System;
using System.Threading.Tasks;
using AutoMapper;
using Coins.Core.Constants;
using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Core.Models.DtoAPI.General;
using Coins.Entities.Domins.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Coins.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UserManager<ApplicationUser> _userManager;
        protected readonly IConfiguration _configuration;
        protected IHttpContextAccessor _httpContextAccessor;
        protected readonly IMapper _mapper;
        protected APIResponse _response;

        protected bool IsArabic { get; }
        protected string LoginUser { get; }
        public BaseController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _response = new APIResponse();
            if (_httpContextAccessor.HttpContext.Items.TryGetValue("ClientLang", out var lang))
            {
                if (lang != null)
                    IsArabic = lang.ToString().StartsWith("ar");
            }
            if (_httpContextAccessor.HttpContext.Items.TryGetValue("LoginUser", out var loginUser))
            {
                if (loginUser != null)
                    LoginUser = loginUser.ToString();
            }
        }

        [NonAction]
        public IActionResult GetErrorResponse()
        {
            _response.Status = false;
            _response.Message = "Sorry! an error occurred, please call technical support.";
            return Ok(_response);
        }

        [NonAction]
        public IActionResult GetResponse(ResponseMessages responseType)
        {
            _response.Status = false;
            _response.Message = responseType.GetDescription();
            return Ok(_response);
        }

        [NonAction]
        public Guid GetCurrentUserId()
        {
            string userId = User.FindFirst("cuserid").Value;
            return new Guid(userId);
        }

        [NonAction]
        public async Task<ApplicationUser> GetCurrentUser()
        {
            var userItem = await _userManager.FindByIdAsync(GetCurrentUserId().ToString());
            return userItem;
        }

        [NonAction]
        public IActionResult GetResponse(ResponseMessages responseType, bool status)
        {
            _response.Status = status;
            _response.Message = responseType.GetDescription();
            return Ok(_response);
        }

        [NonAction]
        public IActionResult GetResponse(ResponseMessages responseType, bool status, Object data, HttpResponseCode code = HttpResponseCode.Ok)
        {
            _response.Status = status;
            _response.Message = responseType.GetDescription();
            _response.Data = data;
            return GetTypeOfReponse(_response, code);
        }

        [NonAction]
        public IActionResult GetResponse(string message, bool status, Object data, HttpResponseCode code = HttpResponseCode.Ok)
        {
            _response.Status = status;
            _response.Message = message;
            _response.Data = data;
            return GetTypeOfReponse(_response, code);
        }

        [NonAction]
        public IActionResult GetResponse(string responseType, bool status)
        {
            _response.Status = status;
            _response.Message = responseType;
            return Ok(_response);
        }

        private IActionResult GetTypeOfReponse(object _response, HttpResponseCode code)
        {
            if (code == HttpResponseCode.Ok)
            {
                return Ok(_response);
            }
            else if (code == HttpResponseCode.BadRequest)
            {
                return BadRequest(_response);
            }
            else if (code == HttpResponseCode.NotFound)
            {
                return NotFound(_response);
            }
            else if (code == HttpResponseCode.Unauthorized)
            {
                return Unauthorized(_response);
            }
            else if (code == HttpResponseCode.Forbidden)
            {
                return Forbid();
            }
            return Ok(_response);
        }
    }
}
