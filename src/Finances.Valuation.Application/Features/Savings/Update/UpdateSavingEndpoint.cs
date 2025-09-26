using FastEndpoints;
using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.Shared.Endpoints.Update;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Savings.Update;

internal class UpdateSavingEndpoint(UpdateHandler updateHandler, SavingRepository savingRepository) : Endpoint<SavingDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/savings/{Id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Updates an existing saving entry";
            s.Description = "Updates an existing saving entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(SavingDto request, CancellationToken ct)
    {
        return await updateHandler.HandleAsync(request, (saving) =>
        {
            saving.Name = request.Name;
            saving.Amount = request.Amount;
            saving.TargetAmount = request.TargetAmount;
        }, savingRepository, HttpContext);
    }    
}