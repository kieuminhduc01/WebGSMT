using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebGSMT.Migrations
{
    public partial class admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Catalog_Data",
                columns: table => new
                {
                    TagName = table.Column<string>(nullable: false),
                    DiemDo = table.Column<string>(nullable: false),
                    DeviceName = table.Column<string>(nullable: false),
                    Unit = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog_Data", x => new { x.TagName, x.DiemDo });
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    BranchOrProtocol = table.Column<string>(nullable: false),
                    NameShow = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Parent = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleName);
                });

            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    TagName = table.Column<string>(nullable: false),
                    DeviceName = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Connected = table.Column<bool>(nullable: false),
                    DiemDo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => new { x.TagName, x.DeviceName, x.Time });
                    table.ForeignKey(
                        name: "FK_Data_Device_DeviceName",
                        column: x => x.DeviceName,
                        principalTable: "Device",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Data_Catalog_Data_TagName_DiemDo",
                        columns: x => new { x.TagName, x.DiemDo },
                        principalTable: "Catalog_Data",
                        principalColumns: new[] { "TagName", "DiemDo" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Account_Role",
                columns: table => new
                {
                    RoleName = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_Role", x => new { x.UserName, x.RoleName });
                    table.ForeignKey(
                        name: "FK_Account_Role_Role_RoleName",
                        column: x => x.RoleName,
                        principalTable: "Role",
                        principalColumn: "RoleName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Account_Role_Account_UserName",
                        column: x => x.UserName,
                        principalTable: "Account",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permission_Role",
                columns: table => new
                {
                    RoleName = table.Column<string>(nullable: false),
                    PermissionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission_Role", x => new { x.PermissionID, x.RoleName });
                    table.ForeignKey(
                        name: "FK_Permission_Role_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permission_Role_Role_RoleName",
                        column: x => x.RoleName,
                        principalTable: "Role",
                        principalColumn: "RoleName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 1, "#", "Giam Sat" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 20, "17", "Quan Tri Vien-Nguoi Dung-Sua" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 19, "17", "Quan Tri Vien-Nguoi Dung-Xem" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 18, "17", "Quan Tri Vien-Nguoi Dung-Them moi" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 17, "11", "Quan Tri Vien-Nguoi Dung" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 16, "12", "Quan Tri Vien-Vai Tro-Xoa" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 15, "12", "Quan Tri Vien-Vai Tro-Sua" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 14, "12", "Quan Tri Vien-Vai Tro-Xem" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 13, "12", "Quan Tri Vien-Vai Tro-Them moi" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 12, "11", "Quan Tri Vien-Vai Tro" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 11, "1", "Quan Tri Vien" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 10, "7", "Danh muc du lieu-Xoa" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 9, "7", "Danh muc du lieu-Sua" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 8, "7", "Danh muc du lieu-Them moi" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 7, "1", "Danh muc du lieu" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 6, "2", "Thiet bi-Xoa" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 5, "2", "Thiet bi-Sua" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 4, "2", "Thiet bi-Xem" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 3, "2", "Thiet bi-Them moi" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 2, "1", "Thiet bi" });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "ID", "Parent", "Text" },
                values: new object[] { 21, "17", "Quan Tri Vien-Nguoi Dung-Xoa" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleName", "Description" },
                values: new object[] { "Admin", "lam duoc moi thu" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 19, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 18, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 17, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 16, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 15, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 14, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 13, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 12, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 20, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 11, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 9, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 8, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 7, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 6, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 5, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 4, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 3, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 2, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 10, "Admin" });

            migrationBuilder.InsertData(
                table: "Permission_Role",
                columns: new[] { "PermissionID", "RoleName" },
                values: new object[] { 21, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Role_RoleName",
                table: "Account_Role",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_Data_DeviceName",
                table: "Data",
                column: "DeviceName");

            migrationBuilder.CreateIndex(
                name: "IX_Data_TagName_DiemDo",
                table: "Data",
                columns: new[] { "TagName", "DiemDo" });

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Role_RoleName",
                table: "Permission_Role",
                column: "RoleName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account_Role");

            migrationBuilder.DropTable(
                name: "Data");

            migrationBuilder.DropTable(
                name: "Permission_Role");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Catalog_Data");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
