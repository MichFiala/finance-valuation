
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Finances.Valuation.Application.Test")]

namespace Finances.Valuation.Application.Features.Savings.Models;

internal class Saving
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Amount { get; set; }

    public decimal? TargetAmount { get; set; }

    public decimal? ExpectedMonthlyContributionAmount { get; set; }
}

