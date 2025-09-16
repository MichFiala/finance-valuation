using Finances.Valuation.Application.Features.Debts;
using Finances.Valuation.Application.Features.Debts.Models;
using Finances.Valuation.Application.Features.Investments;
using Finances.Valuation.Application.Features.Savings;
using Finances.Valuation.Application.Features.SavingsLongevity.Get.Models;
using Finances.Valuation.Application.Features.Spendings;
using Finances.Valuation.Application.Features.Spendings.Models;

namespace Finances.Valuation.Application.Features.SavingsLongevity;

internal class SavingsLongevityCalculationService(
    SavingRepository savingRepository,
    InvestmentRepository investmentRepository,
    SpendingRepository spendingRepository,
    DebtRepository debtRepository)
{
    public async Task<int> CalculateMonthsOfLongevityAsync()
    {
        IReadOnlyCollection<Spending> totalSpendings = await spendingRepository.GetMandatoryAsync();
        IReadOnlyCollection<Investments.Models.Investment> investments = await investmentRepository.GetAsync();
        IReadOnlyCollection<Debt> debts = await debtRepository.GetAsync();

        decimal yearlySpendingsMonthlySpreaded = totalSpendings
            .Where(s => s.Frequency == Frequency.Yearly)
            .Sum(s => s.Amount) / 12;

        decimal quaterlySpendingsMonthlySpreaded = totalSpendings
            .Where(s => s.Frequency == Frequency.Quaterly)
            .Sum(s => s.Amount) / 4;

        decimal monthlySpendings = totalSpendings
            .Where(s => s.Frequency == Frequency.Monthly)
            .Sum(s => s.Amount);

        var totalSavings = await savingRepository.GetAsync();

        decimal savingsAmount = totalSavings.Sum(s => s.Amount);
        decimal investmentsAmount = investments.Sum(i => i.Amount);
        decimal debtsPayments = debts.Sum(d => d.Payment);

        decimal remainingSavings = savingsAmount + investmentsAmount;

        int monthsOfLongevity = 0;
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        while (remainingSavings > 0)
        {
            remainingSavings -= monthlySpendings;
            remainingSavings -= yearlySpendingsMonthlySpreaded;
            remainingSavings -= quaterlySpendingsMonthlySpreaded;

            remainingSavings -= debtsPayments;

            currentDate = currentDate.AddMonths(1);
            monthsOfLongevity++;
        }
        return monthsOfLongevity;
    }

    public static SavingsLongevityGrade ValuateGrade(int months)
    {
        return months switch
        {
            <= 1 => SavingsLongevityGrade.Critical,
            > 1 and <= 2 => SavingsLongevityGrade.Insufficient,
            > 3 and <= 5 => SavingsLongevityGrade.NeedsImprovement,
            6 => SavingsLongevityGrade.Recommended,
            > 7 and <= 11 => SavingsLongevityGrade.Strong,
            _ => SavingsLongevityGrade.Excellent
        };
    }
}