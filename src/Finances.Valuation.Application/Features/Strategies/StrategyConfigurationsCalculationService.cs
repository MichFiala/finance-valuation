using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.Strategies;

internal static class StrategyConfigurationsCalculationService
{
    public static IEnumerable<StrategyConfigurationCalculationStepDto> Calculate(
        IReadOnlyList<StrategyConfigurationDto> strategyConfigurations, Income income)
    {
        decimal availableAmount = income.Amount;

        int configurationIndex = 0;
        while (availableAmount > 0 && configurationIndex < strategyConfigurations.Count)
        {
            StrategyConfigurationDto configuration = strategyConfigurations[configurationIndex];

            decimal? contributionAmount = configuration.MonthlyContributionAmount;

            if (configuration.MonthlyContributionAmount is null)
                contributionAmount = configuration.MonthlyContributionPercentage is not null ? income.Amount * configuration.MonthlyContributionPercentage : availableAmount;

            if (contributionAmount > availableAmount)
                contributionAmount = availableAmount;

            yield return new StrategyConfigurationCalculationStepDto
            {
                Id = configuration.Id,
                Name = configuration.Name,
                AccountName = configuration.AccountName,
                Type = configuration.Type,
                AvailableAmount = availableAmount,
                MonthlyExpectedContributionAmount = configuration.MonthlyContributionAmount,
                MonthlyExpectedContributionPercentage = configuration.MonthlyContributionPercentage,
                MonthlyActualContributionAmount = contributionAmount!.Value,
                MonthlyActualContributionPercentage = contributionAmount.Value / income.Amount,
                ReferenceId = configuration.ReferenceId
            };

            availableAmount -= contributionAmount!.Value;

            configurationIndex++;
        }
    }

    public static IEnumerable<StrategyConfigurationCalculationByAccountDto> GroupByAccountName(IEnumerable<StrategyConfigurationCalculationStepDto> steps)
    {
        var grouped = steps.GroupBy(s => s.AccountName)
            .Select(g => new StrategyConfigurationCalculationByAccountDto
            {
                AccountName = g.Key ?? string.Empty,
                TotalMonthlyActualContributionAmount = g.Sum(s => s.MonthlyActualContributionAmount),
                TotalMonthlyActualContributionPercentage = g.Sum(s => s.MonthlyActualContributionPercentage)
            });

        if(grouped.Count() == 1 && string.IsNullOrEmpty(grouped.First().AccountName))
            return [];
        
        return grouped;
    }
}
