using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebGSMT.Migrations
{
    public partial class ducTa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "UserName", "Active", "DOB", "Email", "FullName", "Password", "PhoneNumber" },
                values: new object[] { "duc_ta_vl", true, new DateTime(1999, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duckmhe130998@fpt.edu.vn", "Ta Vu Anh Duc", "123456789", "0377398442" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "UserName",
                keyValue: "duc_ta_vl");
        }
    }
}
