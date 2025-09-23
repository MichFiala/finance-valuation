using FastEndpoints;
using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.Shared.Endpoints.Create;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Savings.Create;

internal class CreateSavingEndpoint(CreateHandler createHandler, SavingRepository savingRepository) : Endpoint<SavingDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/savings");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Creates a new saving entry";
            s.Description = "Creates a new saving entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(SavingDto request, CancellationToken ct)
        => await createHandler.HandleAsync(request, Saving.Create, savingRepository, HttpContext);    
}
