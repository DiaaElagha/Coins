using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coins.Data.Migrations
{
    public partial class fixaddrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgRate",
                table: "Stores");

            migrationBuilder.AddColumn<double>(
                name: "AvgRate",
                table: "StoreBranchs",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                columns: new[] { "ConcurrencyStamp", "CreateAt", "PasswordHash" },
                values: new object[] { "4a318e44-835a-4340-92b2-f559e50b81ef", new DateTimeOffset(new DateTime(2021, 12, 21, 20, 20, 9, 828, DateTimeKind.Unspecified).AddTicks(661), new TimeSpan(0, 2, 0, 0, 0)), "AIaxISGu0Ck71LVassW63HRL49rXHfg7zX8XJ2JFBDW3/WesTdyzzFQQ2yByxN+HDA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgRate",
                table: "StoreBranchs");

            migrationBuilder.AddColumn<double>(
                name: "AvgRate",
                table: "Stores",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                columns: new[] { "ConcurrencyStamp", "CreateAt", "PasswordHash" },
                values: new object[] { "4210c407-08fa-404b-9419-139aaa853f42", new DateTimeOffset(new DateTime(2021, 12, 18, 22, 22, 28, 865, DateTimeKind.Unspecified).AddTicks(3183), new TimeSpan(0, 2, 0, 0, 0)), "ACyw1nrC3McjyfeAMfE8GIQ5nKScjg/ikxWtDqBaJXCDMluHlfkGzD5/qkd8CnZuFQ==" });
        }
    }
}
