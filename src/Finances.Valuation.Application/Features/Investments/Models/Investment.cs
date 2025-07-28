namespace Finances.Valuation.Application.Features.Investments.Models;

internal class Investment
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }
}
