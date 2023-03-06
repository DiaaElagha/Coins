using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coins.Data.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationTests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location = table.Column<Point>(type: "geometry", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreBranchsAdvantages",
                columns: table => new
                {
                    AdvantageId = table.Column<int>(type: "integer", nullable: false),
                    StoreBranchId = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreBranchsAdvantages", x => new { x.AdvantageId, x.StoreBranchId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advantages",
                columns: table => new
                {
                    AdvantageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvantageTitleAr = table.Column<string>(type: "text", nullable: true),
                    AdvantageTitleEn = table.Column<string>(type: "text", nullable: true),
                    IconImageId = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advantages", x => x.AdvantageId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Messege = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralSettings",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GooglePlayMrCoinsAppLink = table.Column<string>(type: "text", nullable: true),
                    AppleStoreMrCoinsAppLink = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSettings", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "MessageSMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    ReceverId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSMS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MrCoinsSocialMedia",
                columns: table => new
                {
                    MrCoinsSocialMediaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SocialType = table.Column<int>(type: "integer", nullable: false),
                    UrlLink = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MrCoinsSocialMedia", x => x.MrCoinsSocialMediaId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    SendDateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    NotificationType = table.Column<int>(type: "integer", nullable: false),
                    ReceverId = table.Column<Guid>(type: "uuid", nullable: true),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialTypesStores",
                columns: table => new
                {
                    StoreBranchId = table.Column<int>(type: "integer", nullable: false),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    SocialType = table.Column<int>(type: "integer", nullable: false),
                    RewardType = table.Column<int>(type: "integer", nullable: false),
                    UrlLink = table.Column<string>(type: "text", nullable: true),
                    RewardNumberOfCoins = table.Column<int>(type: "integer", nullable: true),
                    VoucherId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialTypesStores", x => new { x.StoreId, x.SocialType, x.StoreBranchId });
                });

            migrationBuilder.CreateTable(
                name: "StoreBranchs",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchNameAr = table.Column<string>(type: "text", nullable: true),
                    BranchNameEn = table.Column<string>(type: "text", nullable: true),
                    BranchDescriptionAr = table.Column<string>(type: "text", nullable: true),
                    BranchDescriptionEn = table.Column<string>(type: "text", nullable: true),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    NumOfVisit = table.Column<int>(type: "integer", nullable: false),
                    IsMainBranch = table.Column<bool>(type: "boolean", nullable: false),
                    BranchMainAttachmentId = table.Column<string>(type: "text", nullable: true),
                    BranchLatitudeLocation = table.Column<double>(type: "double precision", nullable: true),
                    BranchLongitudeLocation = table.Column<double>(type: "double precision", nullable: true),
                    Location = table.Column<Point>(type: "geometry", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreBranchs", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "StoreCategory",
                columns: table => new
                {
                    StoreCategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreCategoryNameAr = table.Column<string>(type: "text", nullable: true),
                    StoreCategoryNameEn = table.Column<string>(type: "text", nullable: true),
                    StoreCategoryAttachmentId = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCategory", x => x.StoreCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "StorePriceType",
                columns: table => new
                {
                    StorePriceTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StorePriceTypeAr = table.Column<string>(type: "text", nullable: true),
                    StorePriceTypeEn = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePriceType", x => x.StorePriceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StoreProducts",
                columns: table => new
                {
                    StoreProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreProductNameAr = table.Column<string>(type: "text", nullable: true),
                    StoreProductNameEn = table.Column<string>(type: "text", nullable: true),
                    StoreProductDescriptionAr = table.Column<string>(type: "text", nullable: true),
                    StoreProductDescriptionEn = table.Column<string>(type: "text", nullable: true),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    StoreProductPrice = table.Column<float>(type: "real", nullable: true),
                    StoreProductMainAttachmentId = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProducts", x => x.StoreProductId);
                });

            migrationBuilder.CreateTable(
                name: "StoreRate",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreBranchId = table.Column<int>(type: "integer", nullable: false),
                    RateValue = table.Column<double>(type: "double precision", nullable: false),
                    StoresStoreId = table.Column<int>(type: "integer", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreRate", x => new { x.UserId, x.StoreBranchId });
                    table.ForeignKey(
                        name: "FK_StoreRate_StoreBranchs_StoreBranchId",
                        column: x => x.StoreBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreNameAr = table.Column<string>(type: "text", nullable: true),
                    StoreNameEn = table.Column<string>(type: "text", nullable: true),
                    StoreDescriptionAr = table.Column<string>(type: "text", nullable: true),
                    StoreDescriptionEn = table.Column<string>(type: "text", nullable: true),
                    StoreCategoryId = table.Column<int>(type: "integer", nullable: true),
                    StorePriceTypeId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublish = table.Column<bool>(type: "boolean", nullable: false),
                    AvgRate = table.Column<double>(type: "double precision", nullable: false),
                    LogoImageId = table.Column<string>(type: "text", nullable: true),
                    FirstTimeVoucherId = table.Column<int>(type: "integer", nullable: true),
                    IsActiveFirstTimeVoucher = table.Column<bool>(type: "boolean", nullable: false),
                    DefineNumOfReferralCustomerCoins = table.Column<int>(type: "integer", nullable: false),
                    ReferralFriendVoucherId = table.Column<int>(type: "integer", nullable: true),
                    IsActiveReferralFriend = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiedCoinsAfterDays = table.Column<int>(type: "integer", nullable: true),
                    IsActiveExpiedCoins = table.Column<bool>(type: "boolean", nullable: false),
                    DefineCurrentCurrencyToCoins = table.Column<float>(type: "real", nullable: false),
                    IsActiveVoucherDiscount = table.Column<bool>(type: "boolean", nullable: false),
                    NumberRedeemedCoinsForEveryCurrency = table.Column<int>(type: "integer", nullable: false),
                    VoucherDiscountExpiredAfterDay = table.Column<int>(type: "integer", nullable: true),
                    IsActiveVoucherDiscountExpiredAfterDay = table.Column<bool>(type: "boolean", nullable: false),
                    IsActiveMinCoinsRedeemVoucherDiscount = table.Column<bool>(type: "boolean", nullable: false),
                    DefineMinCoinsRedeemVoucherDiscount = table.Column<int>(type: "integer", nullable: false),
                    IsActiveMaxCoinsToSpentVoucherDiscount = table.Column<bool>(type: "boolean", nullable: false),
                    DefineMaxCoinsToSpentVoucherDiscount = table.Column<int>(type: "integer", nullable: false),
                    IsActiveGoogleMapRate = table.Column<bool>(type: "boolean", nullable: false),
                    StoreBranchsBranchId = table.Column<int>(type: "integer", nullable: true),
                    StoreProductsStoreProductId = table.Column<int>(type: "integer", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                    table.ForeignKey(
                        name: "FK_Stores_StoreBranchs_StoreBranchsBranchId",
                        column: x => x.StoreBranchsBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stores_StoreCategory_StoreCategoryId",
                        column: x => x.StoreCategoryId,
                        principalTable: "StoreCategory",
                        principalColumn: "StoreCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stores_StorePriceType_StorePriceTypeId",
                        column: x => x.StorePriceTypeId,
                        principalTable: "StorePriceType",
                        principalColumn: "StorePriceTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stores_StoreProducts_StoreProductsStoreProductId",
                        column: x => x.StoreProductsStoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "StoreProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    Birthdate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    StoreBranchId = table.Column<int>(type: "integer", nullable: true),
                    StoreId = table.Column<int>(type: "integer", nullable: true),
                    LastUserLatitudeLocation = table.Column<double>(type: "double precision", nullable: true),
                    LastUserLongitudeLocation = table.Column<double>(type: "double precision", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    LastLogin = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FacebookId = table.Column<string>(type: "text", nullable: true),
                    DeviceId = table.Column<string>(type: "text", nullable: true),
                    CodeHash = table.Column<byte[]>(type: "bytea", nullable: true),
                    CodeSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    FcmToken = table.Column<string>(type: "text", nullable: true),
                    AccessToken = table.Column<string>(type: "text", nullable: true),
                    ForgetPasswordCode = table.Column<int>(type: "integer", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "boolean", nullable: false),
                    IsVerify = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_StoreBranchs_StoreBranchId",
                        column: x => x.StoreBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoresBranchsAttachments",
                columns: table => new
                {
                    StoreBranchAttachmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttachmentsId = table.Column<string>(type: "text", nullable: false),
                    StoreBranchId = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoresBranchsAttachments", x => x.StoreBranchAttachmentId);
                    table.ForeignKey(
                        name: "FK_StoresBranchsAttachments_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoresBranchsAttachments_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoresBranchsAttachments_StoreBranchs_StoreBranchId",
                        column: x => x.StoreBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoresProductsAttachments",
                columns: table => new
                {
                    StoreProductAttachmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttachmentsId = table.Column<string>(type: "text", nullable: false),
                    StoreProductId = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoresProductsAttachments", x => x.StoreProductAttachmentId);
                    table.ForeignKey(
                        name: "FK_StoresProductsAttachments_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoresProductsAttachments_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoresProductsAttachments_StoreProducts_StoreProductId",
                        column: x => x.StoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "StoreProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCoins",
                columns: table => new
                {
                    UserCoinId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    StoreBranchId = table.Column<int>(type: "integer", nullable: true),
                    SocialType = table.Column<int>(type: "integer", nullable: true),
                    NumberOfCoinCollected = table.Column<int>(type: "integer", nullable: false),
                    Remaining = table.Column<int>(type: "integer", nullable: false),
                    CoinStartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CoinExpiryDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDetected = table.Column<bool>(type: "boolean", nullable: false),
                    InvoiceValue = table.Column<float>(type: "real", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCoins", x => x.UserCoinId);
                    table.ForeignKey(
                        name: "FK_UserCoins_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCoins_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCoins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCoins_StoreBranchs_StoreBranchId",
                        column: x => x.StoreBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCoins_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogTitle = table.Column<string>(type: "text", nullable: true),
                    LogDescription = table.Column<string>(type: "text", nullable: true),
                    StoreBranchId = table.Column<int>(type: "integer", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_UserLogs_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLogs_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLogs_StoreBranchs_StoreBranchId",
                        column: x => x.StoreBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSocialLogin",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SocialType = table.Column<int>(type: "integer", nullable: false),
                    LoginToken = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSocialLogin", x => new { x.UserId, x.SocialType });
                    table.ForeignKey(
                        name: "FK_UserSocialLogin_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSocialLogin_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSocialLogin_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSocialStore",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreBranchId = table.Column<int>(type: "integer", nullable: false),
                    SocialType = table.Column<int>(type: "integer", nullable: false),
                    NumOfCoins = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSocialStore", x => new { x.UserId, x.StoreBranchId });
                    table.ForeignKey(
                        name: "FK_UserSocialStore_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSocialStore_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSocialStore_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSocialStore_StoreBranchs_StoreBranchId",
                        column: x => x.StoreBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStores",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<int>(type: "integer", nullable: false),
                    TotalCoins = table.Column<int>(type: "integer", nullable: false),
                    NumOfVisitStore = table.Column<int>(type: "integer", nullable: false),
                    ReferrralCode = table.Column<string>(type: "text", nullable: true),
                    LastVisitAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastVisitStoreBranchId = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStores", x => new { x.UserId, x.StoreId });
                    table.ForeignKey(
                        name: "FK_UserStores_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStores_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStores_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserStores_StoreBranchs_LastVisitStoreBranchId",
                        column: x => x.LastVisitStoreBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStores_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoucherNameAr = table.Column<string>(type: "text", nullable: true),
                    VoucherNameEn = table.Column<string>(type: "text", nullable: true),
                    VoucherDescrptionAr = table.Column<string>(type: "text", nullable: true),
                    VoucherDescrptionEn = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StoreId = table.Column<int>(type: "integer", nullable: true),
                    NumOfCoins = table.Column<int>(type: "integer", nullable: false),
                    VoucherExpiredAfterDay = table.Column<int>(type: "integer", nullable: false),
                    VoucherMainAttachmentId = table.Column<string>(type: "text", nullable: true),
                    VoucherDiscountValue = table.Column<float>(type: "real", nullable: true),
                    VoucherTerms = table.Column<string>(type: "text", nullable: true),
                    VoucherType = table.Column<int>(type: "integer", nullable: false),
                    StoreProductsStoreProductId = table.Column<int>(type: "integer", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.VoucherId);
                    table.ForeignKey(
                        name: "FK_Vouchers_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vouchers_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vouchers_StoreProducts_StoreProductsStoreProductId",
                        column: x => x.StoreProductsStoreProductId,
                        principalTable: "StoreProducts",
                        principalColumn: "StoreProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vouchers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserVouchers",
                columns: table => new
                {
                    UserVoucherId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VoucherId = table.Column<int>(type: "integer", nullable: false),
                    VoucherStatus = table.Column<int>(type: "integer", nullable: false),
                    VoucherStartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsRedeem = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVouchers", x => x.UserVoucherId);
                    table.ForeignKey(
                        name: "FK_UserVouchers_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserVouchers_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserVouchers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVouchers_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccessToken", "Address", "Birthdate", "CodeHash", "CodeSalt", "ConcurrencyStamp", "CreateAt", "DeviceId", "Email", "EmailConfirmed", "ExpiryDate", "FacebookId", "FcmToken", "ForgetPasswordCode", "FullName", "Gender", "IsActive", "IsAnonymous", "IsVerify", "LastLogin", "LastUserLatitudeLocation", "LastUserLongitudeLocation", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "StoreBranchId", "StoreId", "TwoFactorEnabled", "UpdateAt", "UserName" },
                values: new object[] { new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"), 0, null, null, null, null, null, "e9bfaa2a-9a7b-44bc-8b59-8b4791ad3c42", new DateTimeOffset(new DateTime(2021, 12, 14, 22, 0, 36, 728, DateTimeKind.Unspecified).AddTicks(3588), new TimeSpan(0, 2, 0, 0, 0)), null, null, false, null, null, null, null, "System Admin", 0, true, true, false, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, false, null, null, null, "AG49V8DvEQCtpg0pv1fg95b3sPc0Mng0+zVVy59B1cuoaBMWdYUC+ZCgVU3i35xaDw==", null, false, "Admin", null, null, null, false, null, "mrcoins" });

            migrationBuilder.CreateIndex(
                name: "IX_Advantages_CreateByUserId",
                table: "Advantages",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Advantages_UpdateByUserId",
                table: "Advantages",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StoreBranchId",
                table: "AspNetUsers",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StoreId",
                table: "AspNetUsers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_CreateByUserId",
                table: "ContactUs",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_UpdateByUserId",
                table: "ContactUs",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSettings_CreateByUserId",
                table: "GeneralSettings",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSettings_UpdateByUserId",
                table: "GeneralSettings",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSMS_CreateByUserId",
                table: "MessageSMS",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSMS_ReceverId",
                table: "MessageSMS",
                column: "ReceverId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSMS_UpdateByUserId",
                table: "MessageSMS",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MrCoinsSocialMedia_CreateByUserId",
                table: "MrCoinsSocialMedia",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MrCoinsSocialMedia_UpdateByUserId",
                table: "MrCoinsSocialMedia",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReceverId",
                table: "Notifications",
                column: "ReceverId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialTypesStores_CreateByUserId",
                table: "SocialTypesStores",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialTypesStores_StoreBranchId",
                table: "SocialTypesStores",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialTypesStores_UpdateByUserId",
                table: "SocialTypesStores",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialTypesStores_VoucherId",
                table: "SocialTypesStores",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreBranchs_CreateByUserId",
                table: "StoreBranchs",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreBranchs_StoreId",
                table: "StoreBranchs",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreBranchs_UpdateByUserId",
                table: "StoreBranchs",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreBranchsAdvantages_CreateByUserId",
                table: "StoreBranchsAdvantages",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreBranchsAdvantages_StoreBranchId",
                table: "StoreBranchsAdvantages",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreBranchsAdvantages_UpdateByUserId",
                table: "StoreBranchsAdvantages",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategory_CreateByUserId",
                table: "StoreCategory",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategory_UpdateByUserId",
                table: "StoreCategory",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePriceType_CreateByUserId",
                table: "StorePriceType",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePriceType_UpdateByUserId",
                table: "StorePriceType",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_CreateByUserId",
                table: "StoreProducts",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_StoreId",
                table: "StoreProducts",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_UpdateByUserId",
                table: "StoreProducts",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRate_CreateByUserId",
                table: "StoreRate",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRate_StoreBranchId",
                table: "StoreRate",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRate_StoresStoreId",
                table: "StoreRate",
                column: "StoresStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRate_UpdateByUserId",
                table: "StoreRate",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_CreateByUserId",
                table: "Stores",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_FirstTimeVoucherId",
                table: "Stores",
                column: "FirstTimeVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_ReferralFriendVoucherId",
                table: "Stores",
                column: "ReferralFriendVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreBranchsBranchId",
                table: "Stores",
                column: "StoreBranchsBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreCategoryId",
                table: "Stores",
                column: "StoreCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StorePriceTypeId",
                table: "Stores",
                column: "StorePriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreProductsStoreProductId",
                table: "Stores",
                column: "StoreProductsStoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UpdateByUserId",
                table: "Stores",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresBranchsAttachments_CreateByUserId",
                table: "StoresBranchsAttachments",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresBranchsAttachments_StoreBranchId",
                table: "StoresBranchsAttachments",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresBranchsAttachments_UpdateByUserId",
                table: "StoresBranchsAttachments",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresProductsAttachments_CreateByUserId",
                table: "StoresProductsAttachments",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresProductsAttachments_StoreProductId",
                table: "StoresProductsAttachments",
                column: "StoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoresProductsAttachments_UpdateByUserId",
                table: "StoresProductsAttachments",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCoins_CreateByUserId",
                table: "UserCoins",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCoins_StoreBranchId",
                table: "UserCoins",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCoins_StoreId",
                table: "UserCoins",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCoins_UpdateByUserId",
                table: "UserCoins",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCoins_UserId",
                table: "UserCoins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_CreateByUserId",
                table: "UserLogs",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_StoreBranchId",
                table: "UserLogs",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_UpdateByUserId",
                table: "UserLogs",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialLogin_CreateByUserId",
                table: "UserSocialLogin",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialLogin_UpdateByUserId",
                table: "UserSocialLogin",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialStore_CreateByUserId",
                table: "UserSocialStore",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialStore_StoreBranchId",
                table: "UserSocialStore",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialStore_UpdateByUserId",
                table: "UserSocialStore",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStores_CreateByUserId",
                table: "UserStores",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStores_LastVisitStoreBranchId",
                table: "UserStores",
                column: "LastVisitStoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStores_StoreId",
                table: "UserStores",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStores_UpdateByUserId",
                table: "UserStores",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_CreateByUserId",
                table: "UserVouchers",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_UpdateByUserId",
                table: "UserVouchers",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_UserId",
                table: "UserVouchers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVouchers_VoucherId",
                table: "UserVouchers",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_CreateByUserId",
                table: "Vouchers",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_StoreId",
                table: "Vouchers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_StoreProductsStoreProductId",
                table: "Vouchers",
                column: "StoreProductsStoreProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_UpdateByUserId",
                table: "Vouchers",
                column: "UpdateByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreBranchsAdvantages_Advantages_AdvantageId",
                table: "StoreBranchsAdvantages",
                column: "AdvantageId",
                principalTable: "Advantages",
                principalColumn: "AdvantageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreBranchsAdvantages_AspNetUsers_CreateByUserId",
                table: "StoreBranchsAdvantages",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreBranchsAdvantages_AspNetUsers_UpdateByUserId",
                table: "StoreBranchsAdvantages",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreBranchsAdvantages_StoreBranchs_StoreBranchId",
                table: "StoreBranchsAdvantages",
                column: "StoreBranchId",
                principalTable: "StoreBranchs",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advantages_AspNetUsers_CreateByUserId",
                table: "Advantages",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advantages_AspNetUsers_UpdateByUserId",
                table: "Advantages",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_AspNetUsers_CreateByUserId",
                table: "ContactUs",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_AspNetUsers_UpdateByUserId",
                table: "ContactUs",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSettings_AspNetUsers_CreateByUserId",
                table: "GeneralSettings",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSettings_AspNetUsers_UpdateByUserId",
                table: "GeneralSettings",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSMS_AspNetUsers_CreateByUserId",
                table: "MessageSMS",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSMS_AspNetUsers_ReceverId",
                table: "MessageSMS",
                column: "ReceverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageSMS_AspNetUsers_UpdateByUserId",
                table: "MessageSMS",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MrCoinsSocialMedia_AspNetUsers_CreateByUserId",
                table: "MrCoinsSocialMedia",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MrCoinsSocialMedia_AspNetUsers_UpdateByUserId",
                table: "MrCoinsSocialMedia",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ReceverId",
                table: "Notifications",
                column: "ReceverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_SenderId",
                table: "Notifications",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialTypesStores_AspNetUsers_CreateByUserId",
                table: "SocialTypesStores",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialTypesStores_AspNetUsers_UpdateByUserId",
                table: "SocialTypesStores",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialTypesStores_StoreBranchs_StoreBranchId",
                table: "SocialTypesStores",
                column: "StoreBranchId",
                principalTable: "StoreBranchs",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialTypesStores_Stores_StoreId",
                table: "SocialTypesStores",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialTypesStores_Vouchers_VoucherId",
                table: "SocialTypesStores",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "VoucherId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreBranchs_AspNetUsers_CreateByUserId",
                table: "StoreBranchs",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreBranchs_AspNetUsers_UpdateByUserId",
                table: "StoreBranchs",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreBranchs_Stores_StoreId",
                table: "StoreBranchs",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreCategory_AspNetUsers_CreateByUserId",
                table: "StoreCategory",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreCategory_AspNetUsers_UpdateByUserId",
                table: "StoreCategory",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorePriceType_AspNetUsers_CreateByUserId",
                table: "StorePriceType",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorePriceType_AspNetUsers_UpdateByUserId",
                table: "StorePriceType",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_AspNetUsers_CreateByUserId",
                table: "StoreProducts",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_AspNetUsers_UpdateByUserId",
                table: "StoreProducts",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_Stores_StoreId",
                table: "StoreProducts",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreRate_AspNetUsers_CreateByUserId",
                table: "StoreRate",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreRate_AspNetUsers_UpdateByUserId",
                table: "StoreRate",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreRate_AspNetUsers_UserId",
                table: "StoreRate",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreRate_Stores_StoresStoreId",
                table: "StoreRate",
                column: "StoresStoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_AspNetUsers_CreateByUserId",
                table: "Stores",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_AspNetUsers_UpdateByUserId",
                table: "Stores",
                column: "UpdateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Vouchers_FirstTimeVoucherId",
                table: "Stores",
                column: "FirstTimeVoucherId",
                principalTable: "Vouchers",
                principalColumn: "VoucherId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Vouchers_ReferralFriendVoucherId",
                table: "Stores",
                column: "ReferralFriendVoucherId",
                principalTable: "Vouchers",
                principalColumn: "VoucherId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreBranchs_AspNetUsers_CreateByUserId",
                table: "StoreBranchs");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreBranchs_AspNetUsers_UpdateByUserId",
                table: "StoreBranchs");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreCategory_AspNetUsers_CreateByUserId",
                table: "StoreCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreCategory_AspNetUsers_UpdateByUserId",
                table: "StoreCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_StorePriceType_AspNetUsers_CreateByUserId",
                table: "StorePriceType");

            migrationBuilder.DropForeignKey(
                name: "FK_StorePriceType_AspNetUsers_UpdateByUserId",
                table: "StorePriceType");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_AspNetUsers_CreateByUserId",
                table: "StoreProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_AspNetUsers_UpdateByUserId",
                table: "StoreProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_AspNetUsers_CreateByUserId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_AspNetUsers_UpdateByUserId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_AspNetUsers_CreateByUserId",
                table: "Vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_AspNetUsers_UpdateByUserId",
                table: "Vouchers");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_StoreBranchs_StoreBranchsBranchId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_Stores_StoreId",
                table: "StoreProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Stores_StoreId",
                table: "Vouchers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "GeneralSettings");

            migrationBuilder.DropTable(
                name: "LocationTests");

            migrationBuilder.DropTable(
                name: "MessageSMS");

            migrationBuilder.DropTable(
                name: "MrCoinsSocialMedia");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SocialTypesStores");

            migrationBuilder.DropTable(
                name: "StoreBranchsAdvantages");

            migrationBuilder.DropTable(
                name: "StoreRate");

            migrationBuilder.DropTable(
                name: "StoresBranchsAttachments");

            migrationBuilder.DropTable(
                name: "StoresProductsAttachments");

            migrationBuilder.DropTable(
                name: "UserCoins");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "UserSocialLogin");

            migrationBuilder.DropTable(
                name: "UserSocialStore");

            migrationBuilder.DropTable(
                name: "UserStores");

            migrationBuilder.DropTable(
                name: "UserVouchers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Advantages");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "StoreBranchs");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "StoreCategory");

            migrationBuilder.DropTable(
                name: "StorePriceType");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "StoreProducts");
        }
    }
}
