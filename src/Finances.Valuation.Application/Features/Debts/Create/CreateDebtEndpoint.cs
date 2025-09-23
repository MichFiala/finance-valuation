using FastEndpoints;
using Finances.Valuation.Application.Features.Debts.Models;
using Finances.Valuation.Application.Features.Shared.Endpoints.Create;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Debts.Create;

internal class CreateDebtEndpoint(CreateHandler createHandler, DebtRepository debtRepository) : Endpoint<DebtDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/debts");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Creates a new debt";
            s.Description = "Creates a new debt entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(DebtDto request, CancellationToken ct)
        => await createHandler.HandleAsync(request, Debt.Create, debtRepository, HttpContext);
}