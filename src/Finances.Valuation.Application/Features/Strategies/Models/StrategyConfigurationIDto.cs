using System.Text.Json.Serialization;

namespace Finances.Valuation.Application.Features.Strategies.Models;

public class StrategyConfigurationDto
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StrategyConfigurationType Type { get; set; }

    public int ReferenceId { get; set; }
    
    internal static StrategyConfigurationDto Create(StrategyConfiguration strategyConfiguration)
    {
        return new StrategyConfigurationDto
        {
            Type = strategyConfiguration.Type,
            ReferenceId = strategyConfiguration.Type switch
            {
                StrategyConfigurationType.Debt => strategyConfiguration.DebtId ?? 0,
                StrategyConfigurationType.Investment => strategyConfiguration.InvestmentId ?? 0,
                StrategyConfigurationType.Saving => strategyConfiguration.SavingId ?? 0,
                StrategyConfigurationType.Spending => strategyConfiguration.SpendingId ?? 0,
                _ => throw new NotImplementedException($"Not implemented handler for type {strategyConfiguration.Type}")
            }
        };
    }
}
