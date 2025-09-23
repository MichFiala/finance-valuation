using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Update;
using Finances.Valuation.Application.Features.Spendings.Models;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Spendings.Update;

internal class UpdateSpendingEndpoint(UpdateHandler updateHandler, SpendingRepository spendingRepository) : Endpoint<SpendingDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/spendings/{Id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Updates an existing spending entry";
            s.Description = "Updates an existing spending entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(SpendingDto request, CancellationToken ct)
    {
        return await updateHandler.HandleAsync(request, (spending) =>
        {
            spending.Name = request.Name;
            spending.Amount = request.Amount;
            spending.Frequency = request.Frequency;
            spending.IsMandatory = request.IsMandatory;
        }, spendingRepository, HttpContext);
    }     
}
