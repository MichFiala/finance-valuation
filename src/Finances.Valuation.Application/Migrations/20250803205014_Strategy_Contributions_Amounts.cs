using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Valuation.Application.Migrations
{
    /// <inheritdoc />
    public partial class Strategy_Contributions_Amounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyContributionAmount",
                schema: "fin",
                table: "strategies_configurations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyContributionPercentage",
                schema: "fin",
                table: "strategies_configurations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyContributionAmount",
                schema: "fin",
                table: "strategies_configurations");

            migrationBuilder.DropColumn(
                name: "MonthlyContributionPercentage",
                schema: "fin",
                table: "strategies_configurations");
        }
    }
}
