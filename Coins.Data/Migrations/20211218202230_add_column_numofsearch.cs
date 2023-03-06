using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coins.Data.Migrations
{
    public partial class add_column_numofsearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfVisit",
                table: "StoreBranchs",
                newName: "NumOfSearch");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                columns: new[] { "ConcurrencyStamp", "CreateAt", "PasswordHash" },
                values: new object[] { "4210c407-08fa-404b-9419-139aaa853f42", new DateTimeOffset(new DateTime(2021, 12, 18, 22, 22, 28, 865, DateTimeKind.Unspecified).AddTicks(3183), new TimeSpan(0, 2, 0, 0, 0)), "ACyw1nrC3McjyfeAMfE8GIQ5nKScjg/ikxWtDqBaJXCDMluHlfkGzD5/qkd8CnZuFQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfSearch",
                table: "StoreBranchs",
                newName: "NumOfVisit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8bfad551-a0f3-484d-a8b2-9de2331f6741"),
                columns: new[] { "ConcurrencyStamp", "CreateAt", "PasswordHash" },
                values: new object[] { "e9bfaa2a-9a7b-44bc-8b59-8b4791ad3c42", new DateTimeOffset(new DateTime(2021, 12, 14, 22, 0, 36, 728, DateTimeKind.Unspecified).AddTicks(3588), new TimeSpan(0, 2, 0, 0, 0)), "AG49V8DvEQCtpg0pv1fg95b3sPc0Mng0+zVVy59B1cuoaBMWdYUC+ZCgVU3i35xaDw==" });
        }
    }
}
