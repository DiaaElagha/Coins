using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Controllers
{
    [Route("[Controller]")]
    public class ErrorController : Controller
    {
        [HttpGet]
        [HttpGet("Index")]
        public IActionResult Index(int code, string message)
        {
            ViewBag.Code = code;
            ViewBag.Message = message;
            if (code >= 500 && code < 600)
                return View("Error500");
            return View();
        }
    }
}
