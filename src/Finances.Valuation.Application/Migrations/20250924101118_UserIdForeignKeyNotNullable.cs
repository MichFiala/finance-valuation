using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Valuation.Application.Migrations
{
    /// <inheritdoc />
    public partial class UserIdForeignKeyNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "strategies_configurations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "strategies",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "spendings",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "savings",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "investments",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "incomes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "debts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_debts_AspNetUsers_UserId",
                schema: "fin",
                table: "debts",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_incomes_AspNetUsers_UserId",
                schema: "fin",
                table: "incomes",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_investments_AspNetUsers_UserId",
                schema: "fin",
                table: "investments",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_savings_AspNetUsers_UserId",
                schema: "fin",
                table: "savings",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_spendings_AspNetUsers_UserId",
                schema: "fin",
                table: "spendings",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_strategies_AspNetUsers_UserId",
                schema: "fin",
                table: "strategies",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_strategies_configurations_AspNetUsers_UserId",
                schema: "fin",
                table: "strategies_configurations",
                column: "UserId",
                principalSchema: "fin",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "strategies_configurations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "strategies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "spendings",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "savings",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "investments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "incomes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "fin",
                table: "debts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

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
    }
}
