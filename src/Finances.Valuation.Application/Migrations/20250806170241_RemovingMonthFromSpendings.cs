using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Valuation.Application.Migrations
{
    /// <inheritdoc />
    public partial class RemovingMonthFromSpendings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                schema: "fin",
                table: "spendings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                schema: "fin",
                table: "spendings",
                type: "INTEGER",
                nullable: true);
        }
    }
}
