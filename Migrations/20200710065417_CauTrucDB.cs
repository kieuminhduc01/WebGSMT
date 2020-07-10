using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebGSMT.Migrations
{
    public partial class CauTrucDB : Migration
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
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
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
                    DeviceName = table.Column<string>(nullable: false),
                    Unit = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    WarnningMin = table.Column<double>(nullable: false),
                    WarnningMax = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog_Data", x => x.TagName);
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
                    Parent = table.Column<int>(nullable: false),
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
                    Connected = table.Column<bool>(nullable: false)
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
                        name: "FK_Data_Catalog_Data_TagName",
                        column: x => x.TagName,
                        principalTable: "Catalog_Data",
                        principalColumn: "TagName",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Account_Role_RoleName",
                table: "Account_Role",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_Data_DeviceName",
                table: "Data",
                column: "DeviceName");

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
