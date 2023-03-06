using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coins.Data.Migrations
{
    public partial class addcolumntitle_ar_eninsliderhome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreRate_Stores_StoresStoreId",
                table: "StoreRate");

            migrationBuilder.DropIndex(
                name: "IX_StoreRate_StoresStoreId",
                table: "StoreRate");

            migrationBuilder.DropColumn(
                name: "StoresStoreId",
                table: "StoreRate");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "SliderHome",
                newName: "TitleEn");

            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                table: "SliderHome",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                columns: new[] { "ConcurrencyStamp", "CreateAt", "PasswordHash" },
                values: new object[] { "7ba81007-e195-4ae0-a994-8708f2d66531", new DateTimeOffset(new DateTime(2022, 11, 25, 16, 58, 4, 267, DateTimeKind.Unspecified).AddTicks(8353), new TimeSpan(0, 2, 0, 0, 0)), "ADaYJtNrhaiJQ3eqoYRsvP/5M7zfV/D0RKsMeFDNwGrdcO92W8AkPZqMxfXd/TtNMg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleAr",
                table: "SliderHome");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "SliderHome",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "StoresStoreId",
                table: "StoreRate",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                columns: new[] { "ConcurrencyStamp", "CreateAt", "PasswordHash" },
                values: new object[] { "090f7072-111a-4f19-98cf-b18f9c95fa87", new DateTimeOffset(new DateTime(2022, 11, 8, 22, 21, 37, 781, DateTimeKind.Unspecified).AddTicks(9951), new TimeSpan(0, 2, 0, 0, 0)), "ADYmNhOVgATyRrffM2RWiqa8f0iljLDpQMtx2I9v274EyKFCoPuvKdNx2UD3J2zFDA==" });

            migrationBuilder.CreateIndex(
                name: "IX_StoreRate_StoresStoreId",
                table: "StoreRate",
                column: "StoresStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreRate_Stores_StoresStoreId",
                table: "StoreRate",
                column: "StoresStoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
