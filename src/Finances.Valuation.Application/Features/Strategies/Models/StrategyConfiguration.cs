namespace Finances.Valuation.Application.Features.Strategies.Models;

internal class StrategyConfiguration
{
    public int Id { get; set; }

    public int StrategyId { get; set; }

    public int? DebtId { get; set; }
    public Debt.Models.Debt? Debt { get; set; }

    public int? SavingId { get; set; }
    public Savings.Models.Saving? Saving { get; set; }

    public int? SpendingId { get; set; }
    public Spendings.Models.Spending? Spending { get; set; }

    public int? InvestmentId { get; set; }
    public Investments.Models.Investment? Investment { get; set; }

    public StrategyConfigurationType Type { get; set; }

    public int Priority { get; set; }

    private static IReadOnlyDictionary<StrategyConfigurationType, Action<StrategyConfiguration, int>> AssingFunctions = new Dictionary<StrategyConfigurationType, Action<StrategyConfiguration, int>>
    {
        { StrategyConfigurationType.Debt, (strategyConfiguration, referenceId) => strategyConfiguration.DebtId = referenceId},
        { StrategyConfigurationType.Investment, (strategyConfiguration, referenceId) => strategyConfiguration.InvestmentId = referenceId},
        { StrategyConfigurationType.Saving, (strategyConfiguration, referenceId) => strategyConfiguration.SavingId = referenceId},
        { StrategyConfigurationType.Spending, (strategyConfiguration, referenceId) => strategyConfiguration.SpendingId = referenceId},
    };


    public static StrategyConfiguration Create(int strategyId, StrategyConfigurationType type, int referenceId)
    {
        StrategyConfiguration strategyConfiguration = new StrategyConfiguration
        {
            StrategyId = strategyId,
            Type = type
        };

        if (AssingFunctions.TryGetValue(type, out Action<StrategyConfiguration, int>? assignFunction))
            assignFunction(strategyConfiguration, referenceId);
        else
            throw new NotImplementedException($"Not implemented handler for type {type}");

        return strategyConfiguration;
    }

    public static StrategyConfiguration Create(Strategy strategy, StrategyConfigurationDto itemDto) =>
        Create(strategy.Id, itemDto.Type, itemDto.ReferenceId);
}
