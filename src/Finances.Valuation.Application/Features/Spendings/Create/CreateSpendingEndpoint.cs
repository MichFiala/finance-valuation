using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Create;
using Finances.Valuation.Application.Features.Spendings.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Spendings.Create;

internal class CreateSpendingEndpoint(CreateHandler createHandler, SpendingRepository spendingRepository) : Endpoint<SpendingDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/spendings");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Creates a new spending entry";
            s.Description = "Creates a new spending entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(SpendingDto request, CancellationToken ct) => await createHandler.HandleAsync(request, Spending.Create, spendingRepository, HttpContext);
}
