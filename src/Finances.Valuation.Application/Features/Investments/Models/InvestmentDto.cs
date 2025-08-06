namespace Finances.Valuation.Application.Features.Investments.Models;

public class InvestmentDto
{
    public required string Name { get; set; }
    public decimal Amount { get; set; }

    internal static InvestmentDto Create(Investment investment)
    {
        return new InvestmentDto
        {
            Name = investment.Name,
            Amount = investment.Amount
        };
    }
}
