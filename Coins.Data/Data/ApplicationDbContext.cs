using Coins.Core.Models.Domins.Attachments;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Models.Domins.Test;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Notification;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coins.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stores>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StoreBranchs>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Advantages>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StoreBranchsAdvantages>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StoreCategory>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StorePriceType>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StoreProducts>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StoreRate>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StoresBranchsAttachments>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StoresProductsAttachments>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<StoreUserFavorites>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Voucher>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<SocialTypesStores>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<MrCoinsSocialMedia>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserCoins>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserLogs>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserSocialLogin>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserStores>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserVouchers>().HasQueryFilter(x => !x.IsDeleted);

            #region OneToMany1
            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.NotificationsListRecever)
                  .WithOne(d => d.ApplicationUserRecever)
                  .HasForeignKey(d => d.ReceverId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.NotificationsListSender)
                  .WithOne(d => d.ApplicationUserSender)
                  .HasForeignKey(d => d.SenderId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.UserCoinsList)
                  .WithOne(d => d.UserRelated)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.UserStoresList)
                  .WithOne(d => d.UserStore)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.StoreRateList)
                  .WithOne(d => d.UserRelated)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Stores>().HasMany(p => p.VouchersList)
                  .WithOne(d => d.Store)
                  .HasForeignKey(d => d.StoreId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ApplicationUser>().HasMany(p => p.MessageSMSReceverList)
                  .WithOne(d => d.ApplicationUserRecever)
                  .HasForeignKey(d => d.ReceverId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Stores>().HasMany(p => p.StoreBranchsList)
                  .WithOne(d => d.Store)
                  .HasForeignKey(d => d.StoreId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Stores>().HasMany(p => p.StoreProductsList)
                  .WithOne(d => d.Store)
                  .HasForeignKey(d => d.StoreId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion

            #region OneToMany2

            modelBuilder.Entity<ApplicationUser>().HasOne(d => d.Store)
                    .WithMany(p => p.UsersList)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ApplicationUser>().HasOne(d => d.StoreBranch)
                  .WithMany(p => p.UsersList)
                  .HasForeignKey(d => d.StoreBranchId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            //modelBuilder.Entity<Stores>().HasOne(d => d.ApplicationUserCreate)
            //        .WithMany(p => p.StoresUserCreateList)
            //        .HasForeignKey(d => d.CreateByUserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull);

            #endregion

            #region ManyToMany
            modelBuilder.Entity<UserStores>().HasKey(x => new { x.UserId, x.StoreId });
            modelBuilder.Entity<UserSocialLogin>().HasKey(x => new { x.UserId, x.SocialType });
            modelBuilder.Entity<UserSocialStore>().HasKey(x => new { x.UserId, x.StoreBranchId });
            modelBuilder.Entity<StoreBranchsAdvantages>().HasKey(x => new { x.AdvantageId, x.StoreBranchId });
            modelBuilder.Entity<StoreRate>().HasKey(x => new { x.UserId, x.StoreBranchId });
            modelBuilder.Entity<SocialTypesStores>().HasKey(x => new { x.StoreId, x.SocialType, x.StoreBranchId });
            modelBuilder.Entity<StoreUserFavorites>().HasKey(x => new { x.UserId, x.StoreBranchId });
            #endregion

            modelBuilder.Seed();

            modelBuilder.HasPostgresExtension("postgis");
            base.OnModelCreating(modelBuilder);
        }

        #region User-Auth
        public DbSet<UserCoins> UserCoins { set; get; }
        public DbSet<UserLogs> UserLogs { set; get; }
        public DbSet<UserStores> UserStores { set; get; }
        public DbSet<UserVouchers> UserVouchers { set; get; }
        public DbSet<UserSocialStore> UserSocialStore { set; get; }
        public DbSet<UserSocialLogin> UserSocialLogin { set; get; }
        #endregion

        #region Home
        public DbSet<ContactUs> ContactUs { set; get; }
        public DbSet<GeneralSettings> GeneralSettings { set; get; }
        public DbSet<SliderHome> SliderHome { set; get; }
        #endregion

        #region LookUp & Notifications
        public DbSet<MessageSMS> MessageSMS { set; get; }
        public DbSet<Notifications> Notifications { set; get; }
        #endregion

        #region Social
        public DbSet<MrCoinsSocialMedia> MrCoinsSocialMedia { set; get; }
        public DbSet<SocialTypesStores> SocialTypesStores { set; get; }
        #endregion

        #region StoresInfo
        public DbSet<Advantages> Advantages { set; get; }
        public DbSet<StoreBranchs> StoreBranchs { set; get; }
        public DbSet<StoreBranchsAdvantages> StoreBranchsAdvantages { set; get; }
        public DbSet<StoreCategory> StoreCategory { set; get; }
        public DbSet<StorePriceType> StorePriceType { set; get; }
        public DbSet<StoreRate> StoreRate { set; get; }
        public DbSet<Stores> Stores { set; get; }
        public DbSet<StoresBranchsAttachments> StoresBranchsAttachments { set; get; }
        public DbSet<StoresProductsAttachments> StoresProductsAttachments { set; get; }
        public DbSet<Voucher> Vouchers { set; get; }
        public DbSet<StoreUserFavorites> StoreUserFavorites { set; get; }
        #endregion


        #region Test
        public DbSet<LocationTest> LocationTests { get; set; }
        #endregion
    }
}
