using System.Runtime.CompilerServices;
using Finances.Valuation.Application.Features.Savings.Models;

[assembly: InternalsVisibleTo("Finances.Valuation.Application.Test")]

namespace Finances.Valuation.Application.Features.Debts.Models;

internal class Debt
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DebtType DebtType { get; set; }

    public decimal Amount { get; set; }

    public decimal Interest { get; set; }

    public decimal Payment { get; set; }

    public Saving? Saving { get; set; } = null;

    public int? SavingId { get; set; }
}
