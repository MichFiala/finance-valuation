namespace Finances.Valuation.Application.Features.Spendings.Models;

internal class Spending
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    public Frequency Frequency { get; set; }

    public int? Month { get; set; }
}
