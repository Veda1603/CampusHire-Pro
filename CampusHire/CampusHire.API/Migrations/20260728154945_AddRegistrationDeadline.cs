using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusHire.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRegistrationDeadline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumCGPA",
                table: "EligibilityCriterias",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "RegistrationDeadlines",
                columns: table => new
                {
                    RegistrationDeadlineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationDeadlines", x => x.RegistrationDeadlineId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationDeadlines");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumCGPA",
                table: "EligibilityCriterias",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");
        }
    }
}
