using AutoMapper;
using Coins.Core;
using Coins.Core.Helpers;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Web.Helper;
using Coins.Web.Models;
using Coins.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Controllers
{
    public class ContactUsController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public ContactUsController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,

            IUnitOfWork unitOfWork) : base(configuration, userManager, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> AjaxData([FromBody] dynamic data)
        {
            DataTableHelper d = new DataTableHelper(data);

            var rolesList = await _unitOfWork.ContactUs.Filter(
                totalPages: out var totalPages,
                filter: x => (d.SearchKey == null
                || x.Name.Contains(d.SearchKey)
                || x.Email.Contains(d.SearchKey)
                || x.Messege.Contains(d.SearchKey)
                || x.Phone.Contains(d.SearchKey)
                || x.Subject.Contains(d.SearchKey)),
                orderBy: x => x.OrderByDescending(p => p.CreateAt)).ToListAsync();
           
            var items = rolesList.Select(x => new
            {
                x.Id,
                x.Name,
                x.Phone,
                x.Email,
                x.Subject,
                x.Messege,
                InsertDate = x.CreateAt.Value.ToString("MM/dd/yyyy"),
            }).Skip(d.Start).Take(d.Length).ToList();
            var result =
               new
               {
                   draw = d.Draw,
                   recordsTotal = totalPages,
                   recordsFiltered = totalPages,
                   data = items
               };
            return Json(result);
        }


    }
}
