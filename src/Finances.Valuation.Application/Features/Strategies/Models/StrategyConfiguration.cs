using Finances.Valuation.Application.Features.Shared.Models;

namespace Finances.Valuation.Application.Features.Strategies.Models;

internal class StrategyConfiguration : IUserRelated
{
    public int Id { get; set; }

    public int StrategyId { get; set; }

    public string? AccountName { get; set; }

    public int? DebtId { get; set; }
    public Debts.Models.Debt? Debt { get; set; }

    public int? SavingId { get; set; }
    public Savings.Models.Saving? Saving { get; set; }

    public int? SpendingId { get; set; }
    public Spendings.Models.Spending? Spending { get; set; }

    public int? InvestmentId { get; set; }
    public Investments.Models.Investment? Investment { get; set; }

    public StrategyConfigurationType Type { get; set; }

    public int Priority { get; set; }

    public decimal? MonthlyContributionAmount { get; set; }

    public decimal? MonthlyContributionPercentage { get; set; }

    public required string UserId { get; set; }

    public User.Models.User? User { get; set; }

    private static IReadOnlyDictionary<StrategyConfigurationType, Action<StrategyConfiguration, int>> AssingFunctions = new Dictionary<StrategyConfigurationType, Action<StrategyConfiguration, int>>
    {
        { StrategyConfigurationType.Debt, (strategyConfiguration, referenceId) => strategyConfiguration.DebtId = referenceId},
        { StrategyConfigurationType.Investment, (strategyConfiguration, referenceId) => strategyConfiguration.InvestmentId = referenceId},
        { StrategyConfigurationType.Saving, (strategyConfiguration, referenceId) => strategyConfiguration.SavingId = referenceId},
        { StrategyConfigurationType.Spending, (strategyConfiguration, referenceId) => strategyConfiguration.SpendingId = referenceId},
    };


    public static StrategyConfiguration Create(
        int strategyId,
        string? accountName,
        StrategyConfigurationType type,
        int referenceId,
        int priority,
        decimal? monthlyContributionAmount,
        decimal? monthlyContributionPercentage,
        string userId)
    {
        StrategyConfiguration strategyConfiguration = new()
        {
            StrategyId = strategyId,
            AccountName = accountName,
            Type = type,
            Priority = priority,
            MonthlyContributionAmount = monthlyContributionAmount,
            MonthlyContributionPercentage = monthlyContributionPercentage,
            UserId = userId
        };

        if (AssingFunctions.TryGetValue(type, out Action<StrategyConfiguration, int>? assignFunction))
            assignFunction(strategyConfiguration, referenceId);
        else
            throw new NotImplementedException($"Not implemented handler for type {type}");

        return strategyConfiguration;
    }

    public static StrategyConfiguration Create(Strategy strategy, StrategyConfigurationDto itemDto, int priority) =>
        Create(strategy.Id, itemDto.AccountName, itemDto.Type, itemDto.ReferenceId, priority, itemDto.MonthlyContributionAmount, itemDto.MonthlyContributionPercentage, strategy.UserId);
        
    public static StrategyConfiguration Create(int strategyId, string userId, StrategyConfigurationDto itemDto, int priority) =>
        Create(strategyId, itemDto.AccountName, itemDto.Type, itemDto.ReferenceId, priority, itemDto.MonthlyContributionAmount, itemDto.MonthlyContributionPercentage, userId);
}
