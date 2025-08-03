using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Finances.Valuation.Application.Test")]

namespace Finances.Valuation.Application.Features.Debts;

internal interface IMortgagePayOffForecastService
{
    int ForecastPayOffMonths(Models.Debt debt);
}

internal class MortgagePayOffForecastService : IMortgagePayOffForecastService
{
    public int ForecastPayOffMonths(Models.Debt debt)
    {
        // return SimulatePayoffMonthsWithOffsetAndSavingContribution(debt.Amount, debt.Interest, debt.Payment, debt.Saving?.Amount ?? 0, debt.Saving?.MonthlyContributionAmount ?? 0);
        // const int maximumMonths = 30 * 12;

        // decimal principal = debt.Amount;
        // decimal interest = debt.Interest;
        // decimal payment = debt.Payment;
        // decimal interestMonthly = interest / 12 / 100;

        // decimal offsetSavingAmount = debt.Saving?.Amount ?? 0;
        // decimal monthlyContribution = debt.Saving?.MonthlyContributionAmount ?? 0;

        // int i = 0;
        // while (i < maximumMonths && principal > 0)
        // {
        //     decimal effectivePrincipal = Math.Max(0, principal - offsetSavingAmount);

        //     decimal interestAmount = effectivePrincipal * interestMonthly;
        //     decimal principalPaid = payment - interestAmount;

        //     principal = Math.Max(0, principal - principalPaid); ;
        //     offsetSavingAmount += monthlyContribution;
        //     i++;
        // }

        // return i;

        double monthlyInterestRate = (double)debt.Interest / 100 / 12;
        double principal = (double)debt.Amount - (double)(debt.Saving?.Amount ?? 0);
        double payment = (double)debt.Payment + (double) (debt.Saving?.ExpectedMonthlyContributionAmount ?? 0);

        if (payment <= principal * monthlyInterestRate)
            return int.MaxValue;

        double numberOfMonths = -Math.Log(1 - monthlyInterestRate * principal / payment) / Math.Log(1 + monthlyInterestRate);
        return (int)Math.Ceiling(numberOfMonths);
    }
    
    public int SimulatePayoffMonthsWithOffsetAndSavingContribution(
    decimal originalLoanAmount,
    decimal annualInterestRatePercent,
    decimal monthlyPaymentAmount,
    decimal initialSavingOffset,
    decimal monthlySavingContribution)
    {
        const int maxMonths = 30 * 12; // 30 years

        decimal remainingLoanPrincipal = originalLoanAmount;
        decimal savingBalance = initialSavingOffset;

        decimal monthlyInterestRate = annualInterestRatePercent / 100m / 12m;

        int monthsElapsed = 0;

        while (monthsElapsed < maxMonths && remainingLoanPrincipal > 0)
        {
            // Offset the loan principal with the savings balance
            decimal effectivePrincipal = Math.Max(0, remainingLoanPrincipal - savingBalance);

            // Calculate interest only on the effective (offset) principal
            decimal interestForThisMonth = effectivePrincipal * monthlyInterestRate;

            // Determine how much of the payment goes toward reducing principal
            decimal principalReduction = monthlyPaymentAmount - interestForThisMonth;

            // Avoid infinite loop: if payment doesn't cover interest
            if (principalReduction <= 0)
                return maxMonths;

            // Reduce the principal
            remainingLoanPrincipal = Math.Max(0, remainingLoanPrincipal - principalReduction);

            // Increase savings
            savingBalance += monthlySavingContribution;

            monthsElapsed++;
        }

        return monthsElapsed;
    }

}
