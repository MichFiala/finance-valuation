using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Delete;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Savings.Delete;

internal class DeleteSavingEndpoint(DeleteHandler deleteHandler, SavingRepository savingRepository) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/savings/{id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Deletes a saving";
            s.Description = "Deletes a saving entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");

        return await deleteHandler.HandleAsync(id, savingRepository, HttpContext);
    }
}