namespace Finances.Valuation.Application.Features.Strategies.Models;

public class StrategyConfigurationCalculationByAccountDto
{
    public required string AccountName { get; set; }
    public required decimal TotalMonthlyActualContributionAmount { get; set; }
    public required decimal TotalMonthlyActualContributionPercentage { get; set; }
}
