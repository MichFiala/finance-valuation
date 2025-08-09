using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies;

internal static class StrategyConfigurationsCalculationService
{
    public static IEnumerable<StrategyConfigurationCalculationStepDto> Calculate(IReadOnlyList<StrategyConfigurationDto> strategyConfigurations, Income income)
    {
        decimal availableAmount = income.Amount;

        int configurationIndex = 0;
        while (availableAmount > 0 && configurationIndex < strategyConfigurations.Count)
        {
            StrategyConfigurationDto configuration = strategyConfigurations[configurationIndex];

            decimal? contributionAmount = configuration.MonthlyContributionAmount;
            
            if (configuration.MonthlyContributionAmount is null)
                contributionAmount = configuration.MonthlyContributionPercentage is not null ? income.Amount * configuration.MonthlyContributionPercentage : 0;
            
            if (contributionAmount > availableAmount)
                contributionAmount = availableAmount;

            yield return new StrategyConfigurationCalculationStepDto
            {
                Name = configuration.Name,
                Type = configuration.Type,
                AvailableAmount = availableAmount,
                MonthlyExpectedContributionAmount = configuration.MonthlyContributionAmount,
                MonthlyExpectedContributionPercentage = configuration.MonthlyContributionPercentage,
                MonthlyActualContributionAmount = contributionAmount!.Value,
                MonthlyActualContributionPercentage = contributionAmount.Value / income.Amount
            };

            availableAmount -= contributionAmount!.Value;

            configurationIndex++;
        }
    }
}
