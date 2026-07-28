using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusHire.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEligibilityCriteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EligibilityCriterias",
                columns: table => new
                {
                    EligibilityCriteriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimumCGPA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumBacklogs = table.Column<int>(type: "int", nullable: false),
                    AllowedDepartments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassingYear = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EligibilityCriterias", x => x.EligibilityCriteriaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EligibilityCriterias");
        }
    }
}
