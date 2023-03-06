using Coins.Core.Helpers;
using Coins.Entities.Domins.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants = Coins.Core.Constants;
using Enums = Coins.Core.Constants.Enums;

namespace Coins.Data.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                    FullName = "System Admin",
                    UserName = "mrcoins",
                    PasswordHash = ExtensionMethods.HashPassword("Admin11$"),
                    Gender = Enums.Gender.Male,
                    Role = Constants.UsersRoles.Admin
                });

        }

    }
}

