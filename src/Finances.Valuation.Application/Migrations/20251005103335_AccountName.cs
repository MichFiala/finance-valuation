using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Valuation.Application.Migrations
{
    /// <inheritdoc />
    public partial class AccountName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                schema: "fin",
                table: "strategies_configurations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                schema: "fin",
                table: "strategies_configurations");
        }
    }
}
