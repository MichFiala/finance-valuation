using FastEndpoints;
using Finances.Valuation.Application.Features.Debts.Models;
using Finances.Valuation.Application.Features.Shared.Endpoints.Update;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Debts.Update;
internal class UpdateDebtEndpoint(UpdateHandler updateHandler, DebtRepository debtRepository) : Endpoint<DebtDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/debts/{Id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Updates an existing debt entry";
            s.Description = "Updates an existing debt entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(DebtDto request, CancellationToken ct)
    {
        return await updateHandler.HandleAsync(request, (debt) =>
        {
            debt.Name = request.Name;
            debt.Amount = request.Amount;
            debt.Payment = request.Payment;
            debt.Interest = request.Interest;
        }, debtRepository, HttpContext);
    }
}