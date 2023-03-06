using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Coins.Data.Migrations
{
    public partial class addtablesStoreFavoriteandhomeslider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SliderHome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    AttachmentId = table.Column<string>(type: "text", nullable: true),
                    ButtonTitle = table.Column<string>(type: "text", nullable: true),
                    StoreBranchIdButton = table.Column<int>(type: "integer", nullable: true),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderHome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SliderHome_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SliderHome_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SliderHome_StoreBranchs_StoreBranchIdButton",
                        column: x => x.StoreBranchIdButton,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreUserFavorites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreBranchId = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUserFavorites", x => new { x.UserId, x.StoreBranchId });
                    table.ForeignKey(
                        name: "FK_StoreUserFavorites_AspNetUsers_CreateByUserId",
                        column: x => x.CreateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreUserFavorites_AspNetUsers_UpdateByUserId",
                        column: x => x.UpdateByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreUserFavorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreUserFavorites_StoreBranchs_StoreBranchId",
                        column: x => x.StoreBranchId,
                        principalTable: "StoreBranchs",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                columns: new[] { "ConcurrencyStamp", "CreateAt", "PasswordHash" },
                values: new object[] { "090f7072-111a-4f19-98cf-b18f9c95fa87", new DateTimeOffset(new DateTime(2022, 11, 8, 22, 21, 37, 781, DateTimeKind.Unspecified).AddTicks(9951), new TimeSpan(0, 2, 0, 0, 0)), "ADYmNhOVgATyRrffM2RWiqa8f0iljLDpQMtx2I9v274EyKFCoPuvKdNx2UD3J2zFDA==" });

            migrationBuilder.CreateIndex(
                name: "IX_SliderHome_CreateByUserId",
                table: "SliderHome",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SliderHome_StoreBranchIdButton",
                table: "SliderHome",
                column: "StoreBranchIdButton");

            migrationBuilder.CreateIndex(
                name: "IX_SliderHome_UpdateByUserId",
                table: "SliderHome",
                column: "UpdateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreUserFavorites_CreateByUserId",
                table: "StoreUserFavorites",
                column: "CreateByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreUserFavorites_StoreBranchId",
                table: "StoreUserFavorites",
                column: "StoreBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreUserFavorites_UpdateByUserId",
                table: "StoreUserFavorites",
                column: "UpdateByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SliderHome");

            migrationBuilder.DropTable(
                name: "StoreUserFavorites");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                columns: new[] { "ConcurrencyStamp", "CreateAt", "PasswordHash" },
                values: new object[] { "4a318e44-835a-4340-92b2-f559e50b81ef", new DateTimeOffset(new DateTime(2021, 12, 21, 20, 20, 9, 828, DateTimeKind.Unspecified).AddTicks(661), new TimeSpan(0, 2, 0, 0, 0)), "AIaxISGu0Ck71LVassW63HRL49rXHfg7zX8XJ2JFBDW3/WesTdyzzFQQ2yByxN+HDA==" });
        }
    }
}
