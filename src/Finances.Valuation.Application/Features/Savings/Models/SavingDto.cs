using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Savings.Models;

public class SavingDto : IEntityDto
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required decimal Amount { get; set; }

    public decimal? TargetAmount { get; set; }

    public decimal? ExpectedMonthlyContributionAmount { get; set; }

    internal static SavingDto Create(Saving saving)
    {
        return new SavingDto
        {
            Id = saving.Id,
            Name = saving.Name,
            Amount = saving.Amount,
            TargetAmount = saving.TargetAmount,
            ExpectedMonthlyContributionAmount = saving.ExpectedMonthlyContributionAmount
        };
    }
}
