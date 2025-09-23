using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Valuation.Application.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "strategies_configurations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "strategies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "spendings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "savings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "investments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "incomes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "debts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_strategies_configurations_UserId",
                schema: "fin",
                table: "strategies_configurations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_strategies_UserId",
                schema: "fin",
                table: "strategies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_spendings_UserId",
                schema: "fin",
                table: "spendings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_savings_UserId",
                schema: "fin",
                table: "savings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_investments_UserId",
                schema: "fin",
                table: "investments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_incomes_UserId",
                schema: "fin",
                table: "incomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_debts_UserId",
                schema: "fin",
                table: "debts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_debts_AspNetUsers_UserId",
                schema: "fin",
                table: "debts",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_incomes_AspNetUsers_UserId",
                schema: "fin",
                table: "incomes",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_investments_AspNetUsers_UserId",
                schema: "fin",
                table: "investments",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_savings_AspNetUsers_UserId",
                schema: "fin",
                table: "savings",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_spendings_AspNetUsers_UserId",
                schema: "fin",
                table: "spendings",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_strategies_AspNetUsers_UserId",
                schema: "fin",
                table: "strategies",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_strategies_configurations_AspNetUsers_UserId",
                schema: "fin",
                table: "strategies_configurations",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_debts_AspNetUsers_UserId",
                schema: "fin",
                table: "debts");

            migrationBuilder.DropForeignKey(
                name: "FK_incomes_AspNetUsers_UserId",
                schema: "fin",
                table: "incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_investments_AspNetUsers_UserId",
                schema: "fin",
                table: "investments");

            migrationBuilder.DropForeignKey(
                name: "FK_savings_AspNetUsers_UserId",
                schema: "fin",
                table: "savings");

            migrationBuilder.DropForeignKey(
                name: "FK_spendings_AspNetUsers_UserId",
                schema: "fin",
                table: "spendings");

            migrationBuilder.DropForeignKey(
                name: "FK_strategies_AspNetUsers_UserId",
                schema: "fin",
                table: "strategies");

            migrationBuilder.DropForeignKey(
                name: "FK_strategies_configurations_AspNetUsers_UserId",
                schema: "fin",
                table: "strategies_configurations");

            migrationBuilder.DropIndex(
                name: "IX_strategies_configurations_UserId",
                schema: "fin",
                table: "strategies_configurations");

            migrationBuilder.DropIndex(
                name: "IX_strategies_UserId",
                schema: "fin",
                table: "strategies");

            migrationBuilder.DropIndex(
                name: "IX_spendings_UserId",
                schema: "fin",
                table: "spendings");

            migrationBuilder.DropIndex(
                name: "IX_savings_UserId",
                schema: "fin",
                table: "savings");

            migrationBuilder.DropIndex(
                name: "IX_investments_UserId",
                schema: "fin",
                table: "investments");

            migrationBuilder.DropIndex(
                name: "IX_incomes_UserId",
                schema: "fin",
                table: "incomes");

            migrationBuilder.DropIndex(
                name: "IX_debts_UserId",
                schema: "fin",
                table: "debts");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "fin",
                table: "strategies_configurations");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "fin",
                table: "strategies");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "fin",
                table: "spendings");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "fin",
                table: "savings");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "fin",
                table: "investments");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "fin",
                table: "incomes");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "fin",
                table: "debts");
        }
    }
}
