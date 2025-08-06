namespace Finances.Valuation.Application.Features.Investments.Models;

internal class Investment
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal Amount { get; set; }

    internal static Investment Create(InvestmentDto investmentDto)
    {
        return new Investment
        {
            Name = investmentDto.Name,
            Amount = investmentDto.Amount
        };
    }
}
