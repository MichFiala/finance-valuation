using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Debts;
using Finances.Valuation.Application.Features.Debts.Models;
using Finances.Valuation.Application.Features.Investments;
using Finances.Valuation.Application.Features.Investments.Models;
using Finances.Valuation.Application.Features.Savings;
using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Finances.Valuation.Application.Features.Spendings;
using Finances.Valuation.Application.Features.Spendings.Models;
using Finances.Valuation.Application.Features.Summary.Get.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Summary.Get;

internal class GetSummaryEndpoint(
    UserManager<User.Models.User> userManager,
    DebtRepository debtRepository,
    SavingRepository savingsRepository,
    InvestmentRepository investmentRepository,
    SpendingRepository spendingRepository) : Endpoint<EmptyRequest, GetSummaryResponse>
{
    public override void Configure()
    {
        Get("/summary");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets summary data";
            s.Description = "Returns a summary of all financial data";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");        

        IReadOnlyCollection<Debt>? debts = await debtRepository.GetAsync(user.Id);

        IReadOnlyCollection<Saving>? savings = await savingsRepository.GetAsync(user.Id);

        IReadOnlyCollection<Investment>? investments = await investmentRepository.GetAsync(user.Id);

        IReadOnlyCollection<Spending>? spendings = await spendingRepository.GetMandatoryAsync(user.Id);

        decimal yearlySpendingsMonthlySpreaded = spendings
            .Where(s => s.Frequency == Frequency.Yearly)
            .Sum(s => s.Amount) / 12;

        decimal quaterlySpendingsMonthlySpreaded = spendings
            .Where(s => s.Frequency == Frequency.Quaterly)
            .Sum(s => s.Amount) / 4;

        decimal monthlySpendings = spendings
            .Where(s => s.Frequency == Frequency.Monthly)
            .Sum(s => s.Amount);

        decimal totalMonthlySpendings = monthlySpendings + quaterlySpendingsMonthlySpreaded + yearlySpendingsMonthlySpreaded;

        await Send.OkAsync(new GetSummaryResponse
        {
            TotalInvestments = investments.Sum(i => i.Amount),
            TotalSavings = savings.Sum(s => s.Amount),
            TotalDebts = debts.Sum(d => d.Amount),
            TotalMonthlySpendings = totalMonthlySpendings,
        }, ct);
    }
}
