using System.Text.Json.Serialization;
namespace Finances.Valuation.Application.Features.Strategies.Models;

public class StrategyConfigurationDto
{
    public required int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StrategyConfigurationType Type { get; set; }

    public int ReferenceId { get; set; }

    public decimal? MonthlyContributionAmount { get; set; }

    public decimal? MonthlyContributionPercentage { get; set; }

    internal static StrategyConfigurationDto Create(StrategyConfiguration strategyConfiguration)
    {
        return strategyConfiguration.Type switch
        {
            StrategyConfigurationType.Debt => CreateDebtStrategyConfigurationDto(strategyConfiguration),
            StrategyConfigurationType.Investment => CreateInvestmentStrategyConfigurationDto(strategyConfiguration),
            StrategyConfigurationType.Saving => CreateSavingStrategyConfigurationDto(strategyConfiguration),
            StrategyConfigurationType.Spending => CreateSpendingStrategyConfigurationDto(strategyConfiguration),
            _ => throw new NotImplementedException($"Not implemented handler for type {strategyConfiguration.Type}")
        };
    }

    private static StrategyConfigurationDto CreateDebtStrategyConfigurationDto(StrategyConfiguration strategyConfiguration)
    {
        if (strategyConfiguration.Debt is null)
        {
            throw new ArgumentNullException(nameof(strategyConfiguration.Debt), "Debt cannot be null for Debt strategy configuration.");
        }

        return new StrategyConfigurationDto
        {
            Id = strategyConfiguration.Id,
            Name = strategyConfiguration.Debt.Name,
            Type = StrategyConfigurationType.Debt,
            MonthlyContributionAmount = strategyConfiguration.MonthlyContributionAmount ?? strategyConfiguration.Debt.Payment,
            ReferenceId = strategyConfiguration.DebtId ?? 0
        };
    }

    private static StrategyConfigurationDto CreateInvestmentStrategyConfigurationDto(StrategyConfiguration strategyConfiguration)
    {
        if (strategyConfiguration.Investment is null)
        {
            throw new ArgumentNullException(nameof(strategyConfiguration.Investment), "Investment cannot be null for Investment strategy configuration.");
        }

        return new StrategyConfigurationDto
        {
            Id = strategyConfiguration.Id,
            Name = strategyConfiguration.Investment.Name,
            Type = StrategyConfigurationType.Investment,
            MonthlyContributionAmount = strategyConfiguration.MonthlyContributionAmount,
            MonthlyContributionPercentage = strategyConfiguration.MonthlyContributionPercentage,
            ReferenceId = strategyConfiguration.InvestmentId ?? 0
        };
    }

    private static StrategyConfigurationDto CreateSavingStrategyConfigurationDto(StrategyConfiguration strategyConfiguration)
    {
        if (strategyConfiguration.Saving is null)
        {
            throw new ArgumentNullException(nameof(strategyConfiguration.Saving), "Saving cannot be null for Saving strategy configuration.");
        }

        return new StrategyConfigurationDto
        {
            Id = strategyConfiguration.Id,
            Name = strategyConfiguration.Saving.Name,
            Type = StrategyConfigurationType.Saving,
            MonthlyContributionAmount = strategyConfiguration.MonthlyContributionAmount ?? strategyConfiguration.Saving.ExpectedMonthlyContributionAmount,
            MonthlyContributionPercentage = strategyConfiguration.MonthlyContributionPercentage,
            ReferenceId = strategyConfiguration.SavingId ?? 0
        };
    }
    
    private static StrategyConfigurationDto CreateSpendingStrategyConfigurationDto(StrategyConfiguration strategyConfiguration)
    {
        if (strategyConfiguration.Spending is null)
        {
            throw new ArgumentNullException(nameof(strategyConfiguration.Spending), "Spending cannot be null for Spending strategy configuration.");
        }

        return new StrategyConfigurationDto
        {
            Id = strategyConfiguration.Id,
            Name = strategyConfiguration.Spending.Name,
            Type = StrategyConfigurationType.Spending,
            MonthlyContributionAmount = strategyConfiguration.Spending.Amount,
            ReferenceId = strategyConfiguration.SpendingId ?? 0
        };
    }
}
