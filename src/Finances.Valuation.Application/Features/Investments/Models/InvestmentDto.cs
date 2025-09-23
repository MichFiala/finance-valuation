using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Investments.Models;

public class InvestmentDto : IEntityDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Amount { get; set; }

    internal static InvestmentDto Create(Investment investment)
    {
        return new InvestmentDto
        {
            Id = investment.Id,
            Name = investment.Name,
            Amount = investment.Amount
        };
    }
}
