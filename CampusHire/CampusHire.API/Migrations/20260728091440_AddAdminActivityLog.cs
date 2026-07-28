using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusHire.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Admins",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminActivityLogs",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminActivityLogs", x => x.ActivityId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminActivityLogs");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Admins");
        }
    }
}
