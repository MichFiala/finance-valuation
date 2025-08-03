using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances.Valuation.Application.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fin");

            migrationBuilder.CreateTable(
                name: "incomes",
                schema: "fin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investment",
                schema: "fin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "savings",
                schema: "fin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TargetAmount = table.Column<decimal>(type: "TEXT", nullable: true),
                    ExpectedMonthlyContributionAmount = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_savings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "spendings",
                schema: "fin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Frequency = table.Column<string>(type: "TEXT", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spendings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "strategies",
                schema: "fin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_strategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "debts",
                schema: "fin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DebtType = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Interest = table.Column<decimal>(type: "TEXT", nullable: false),
                    Payment = table.Column<decimal>(type: "TEXT", nullable: false),
                    SavingId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_debts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_debts_savings_SavingId",
                        column: x => x.SavingId,
                        principalSchema: "fin",
                        principalTable: "savings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "strategies_configurations",
                schema: "fin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StrategyId = table.Column<int>(type: "INTEGER", nullable: false),
                    DebtId = table.Column<int>(type: "INTEGER", nullable: true),
                    SavingId = table.Column<int>(type: "INTEGER", nullable: true),
                    SpendingId = table.Column<int>(type: "INTEGER", nullable: true),
                    InvestmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_strategies_configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_strategies_configurations_Investment_InvestmentId",
                        column: x => x.InvestmentId,
                        principalSchema: "fin",
                        principalTable: "Investment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_strategies_configurations_debts_DebtId",
                        column: x => x.DebtId,
                        principalSchema: "fin",
                        principalTable: "debts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_strategies_configurations_savings_SavingId",
                        column: x => x.SavingId,
                        principalSchema: "fin",
                        principalTable: "savings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_strategies_configurations_spendings_SpendingId",
                        column: x => x.SpendingId,
                        principalSchema: "fin",
                        principalTable: "spendings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_debts_SavingId",
                schema: "fin",
                table: "debts",
                column: "SavingId");

            migrationBuilder.CreateIndex(
                name: "IX_strategies_configurations_DebtId",
                schema: "fin",
                table: "strategies_configurations",
                column: "DebtId");

            migrationBuilder.CreateIndex(
                name: "IX_strategies_configurations_InvestmentId",
                schema: "fin",
                table: "strategies_configurations",
                column: "InvestmentId");

            migrationBuilder.CreateIndex(
                name: "IX_strategies_configurations_SavingId",
                schema: "fin",
                table: "strategies_configurations",
                column: "SavingId");

            migrationBuilder.CreateIndex(
                name: "IX_strategies_configurations_SpendingId",
                schema: "fin",
                table: "strategies_configurations",
                column: "SpendingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "incomes",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "strategies",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "strategies_configurations",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "Investment",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "debts",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "spendings",
                schema: "fin");

            migrationBuilder.DropTable(
                name: "savings",
                schema: "fin");
        }
    }
}
