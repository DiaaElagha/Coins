using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Coins.Api.Filter;
using Coins.Api.Repositories;
using Coins.Api.Services;
using Coins.Api.Settings;
using Coins.Core;
using Coins.Core.Services;
using Coins.Core.Services.ThiedParty;
using Coins.Core.Settings;
using Coins.Data;
using Coins.Data.Data;
using Coins.Data.Repositories;
using Coins.Data.Repositories.ThiedParty;
using Coins.Entities.Domins.Auth;
using Coins.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Coins.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true; //To show detail of error and see the problem

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            #region PostgreSqlConnection
            var postgreSqlConnectionString = Configuration.GetConnectionString("PostgreSqlConnectionString");
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(postgreSqlConnectionString, o => o.UseNetTopologySuite())
            );
            #endregion

            services.AddIdentity<ApplicationUser, ApplicationRole>(
               option =>
               {
                   option.Password.RequireDigit = false;
                   option.Password.RequiredLength = 6;
                   option.Password.RequireNonAlphanumeric = false;
                   option.Password.RequireUppercase = false;
                   option.Password.RequireLowercase = false;
               }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       ValidAudience = Configuration["Jwt:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"]))
                   };
               });

            #region MongoDBConnection
            MongoDBSettings.AttachmentsCollectionName = Configuration["MongoDBSettings:AttachmentsCollectionName"];
            MongoDBSettings.ConnectionString = Configuration["MongoDBSettings:ConnectionString"];
            MongoDBSettings.DatabaseName = Configuration["MongoDBSettings:DatabaseName"];
            #endregion

            FCMSetting.ServerKey = Configuration["FCMSettings:ServerKey"];
            FCMSetting.SenderId = Configuration["FCMSettings:SenderId"];
            FCMSetting.FcmUrl = Configuration["FCMSettings:FcmUrl"];
            FirebaseSettings.App = Configuration["FCMSettings:Secret"];

            JwtSettings.Secret = Configuration["Jwt:SigningKey"];
            JwtSettings.Issuer = Configuration["Jwt:Issuer"];
            JwtSettings.ExpirationInDays = Int16.Parse(Configuration["Jwt:ExpiryInYeras"]);

            services.AddMvc(options =>
            {
                // All endpoints need authorization using our custom authorization filter
                options.Filters.Add(
                    new CustomAuthorizeFilter(
                    new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
                options.Filters.Add(typeof(ValidateModelAttribute));
            });

            services.AddControllers();

            services.AddCors();
            services.AddHttpClient();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API For Mr.Coins",
                    Version = "v1",
                    Description = "Mr.Coins API Documenation",
                });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Scheme = "Bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            #region RegisterServices
            //start api service
            services.AddScoped<IAuthService, AuthRepository>();
            services.AddTransient<IStoresService, StoresService>();
            services.AddTransient<IUserService, UserService>();
            //end api service
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(ISMSSenderService), typeof(SMSSenderRepository));
            services.AddTransient(typeof(INotificationsService), typeof(NotificationRepository));

            services.AddTransient(typeof(ITestService), typeof(TestService));

            services.AddSingleton<StorageService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.Use((context, next) =>
            {
                //get client prefered language
                var defaultLang = "en-GB";
                if (context.Request.Headers.TryGetValue("Accept-Language", out var userLangs))
                {
                    var passedLan = userLangs.ToString().Split(',').FirstOrDefault();
                    if (!string.IsNullOrEmpty(passedLan) && passedLan.StartsWith("ar", StringComparison.OrdinalIgnoreCase))
                    {
                        defaultLang = "ar-SA";
                    }
                }
                //switch UI culture
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLang);
                Thread.CurrentThread.CurrentUICulture.DateTimeFormat = new CultureInfo("en-GB").DateTimeFormat;

                context.Items["ClientLang"] = defaultLang;
                return next();
            });
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tayar Delivery API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
