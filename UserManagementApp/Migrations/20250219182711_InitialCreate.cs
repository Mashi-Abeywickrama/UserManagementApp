using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagementApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_RoleTypes_RoleType",
                        column: x => x.RoleType,
                        principalTable: "RoleTypes",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Statuses_Status",
                        column: x => x.Status,
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "RoleID", "CreatedAt", "ModifiedAt", "RoleName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 19, 18, 27, 11, 466, DateTimeKind.Utc).AddTicks(9426), new DateTime(2025, 2, 19, 18, 27, 11, 466, DateTimeKind.Utc).AddTicks(9446), "Admin" },
                    { 2, new DateTime(2025, 2, 19, 18, 27, 11, 467, DateTimeKind.Utc).AddTicks(253), new DateTime(2025, 2, 19, 18, 27, 11, 467, DateTimeKind.Utc).AddTicks(255), "User" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusID", "CreatedAt", "ModifiedAt", "StatusName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 19, 18, 27, 11, 469, DateTimeKind.Utc).AddTicks(1688), new DateTime(2025, 2, 19, 18, 27, 11, 469, DateTimeKind.Utc).AddTicks(1699), "Active" },
                    { 2, new DateTime(2025, 2, 19, 18, 27, 11, 469, DateTimeKind.Utc).AddTicks(2324), new DateTime(2025, 2, 19, 18, 27, 11, 469, DateTimeKind.Utc).AddTicks(2325), "Inactive" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleType",
                table: "Users",
                column: "RoleType");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Status",
                table: "Users",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
