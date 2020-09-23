
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebGSMT.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "UserName", "Active", "DOB", "Email", "FullName", "Password", "PhoneNumber" },
                values: new object[] { "duckieuola", true, new DateTime(1999, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "duckmhe130998@fpt.edu.vn", "Kieu Minh Duc", "123456789", "0377398442" });

            migrationBuilder.InsertData(
                table: "Account_Role",
                columns: new[] { "UserName", "RoleName" },
                values: new object[] { "duckieuola", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Account_Role",
                keyColumns: new[] { "UserName", "RoleName" },
                keyValues: new object[] { "duckieuola", "Admin" });

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "UserName",
                keyValue: "duckieuola");
        }
    }
}
