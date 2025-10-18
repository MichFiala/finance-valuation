using Finances.Valuation.Application.Features.Debts;
using Finances.Valuation.Application.Features.Incomes;
using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Investments;
using Finances.Valuation.Application.Features.Savings;
using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.SavingsForecast.Get.Models;
using Finances.Valuation.Application.Features.Spendings;
using Finances.Valuation.Application.Features.Strategies;
using Finances.Valuation.Application.Features.Strategies.Models;

namespace Finances.Valuation.Application.Features.SavingsForecast;

internal class SavingsForecastService(
    SavingRepository savingRepository,
    IncomeRepository incomeRepository,
    StrategyRepository strategyRepository)
{
    const int MaxMonths = 600; // 50 years
    public async Task<(int, List<SavingsForecastStepDto>)> CalculateForecastAsync(
        string userId, int savingId, int mainIncomeStrategyId, int sideIncomeStrategyId)
    {
        Saving? saving = await savingRepository.GetAsync(savingId, userId) ?? throw new KeyNotFoundException("Saving not found");

        if (saving.TargetAmount is null)
            throw new InvalidOperationException("Target amount is not set");

        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        IReadOnlyCollection<Income> incomes = await incomeRepository.GetFromDateAsync(userId, currentDate);

        IReadOnlyCollection<StrategyConfiguration>? mainStrategyConfigurations = await strategyRepository.GetByStrategyIdAsync(mainIncomeStrategyId, userId);
        IReadOnlyCollection<StrategyConfiguration>? sideStrategyConfigurations = await strategyRepository.GetByStrategyIdAsync(sideIncomeStrategyId, userId);

        if (mainStrategyConfigurations is null)
            throw new Exception("Strategy configurations not found for {mainIncomeStrategyId}.");

        if (sideStrategyConfigurations is null)
            throw new Exception("Strategy configurations not found for {sideIncomeStrategyId}.");

        decimal targetAmount = saving.TargetAmount.Value;
        decimal currentAmount = saving.Amount;

        List<SavingsForecastStepDto> forecastSteps = new();

        int months = 0;
        DateOnly actualDate = incomes.First().Date;
        foreach (var income in incomes)
        {
            var configurations = mainStrategyConfigurations;

            if (!income.IsMainIncome)
                configurations = sideStrategyConfigurations;

            List<StrategyConfigurationCalculationStepDto> result = StrategyConfigurationsCalculationService.Calculate(configurations.Select(StrategyConfigurationDto.Create).ToList(), income).ToList();

            var calculatedSaving = result.FirstOrDefault(r => r.ReferenceId == saving.Id && r.Type == StrategyConfigurationType.Saving);

            decimal contributedAmount = 0;
            if (calculatedSaving is not null)
                contributedAmount = calculatedSaving.MonthlyActualContributionAmount;

            currentAmount += contributedAmount;

            if (income.Date > actualDate)
            {
                months++;
                actualDate = income.Date;
            }

            forecastSteps.Add(new SavingsForecastStepDto
            {
                IncomeDto = IncomeDto.Create(income),
                CurrentAmount = currentAmount,
                TargetAmountDifference = targetAmount - currentAmount,
                ContributedAmount = contributedAmount,
                StrategyCalculationSteps = result
            });
        }

        if (currentAmount < targetAmount)
            return (MaxMonths, forecastSteps);

        return (months, forecastSteps);
    }
}
