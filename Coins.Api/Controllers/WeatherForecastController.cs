using AutoMapper;
using Coins.Core.Models.Domins.Attachments;
using Coins.Data.Data;
using Coins.Entities.Domins.Auth;
using Coins.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Controllers
{
    [AllowAnonymous]
    public class WeatherForecastController : BaseController
    {
        private readonly ApplicationDbContext context;
        private readonly StorageService storage;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public WeatherForecastController(
            ApplicationDbContext context,
            StorageService storage,

            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(configuration, userManager, httpContextAccessor, mapper)
        {
            this.context = context;
            this.storage = storage;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var listData = await context.Users.ToListAsync();
            return Ok(listData);
        }


        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<string>> Upload(IFormFile file)
        {
            try
            {
                var attachmentResult = await storage.UploadFile(file);
                if (attachmentResult is not null)
                    return Ok(attachmentResult);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Attachments>>> GetFiles()
        {
            return await storage.GetAll();
        }


    }
}
