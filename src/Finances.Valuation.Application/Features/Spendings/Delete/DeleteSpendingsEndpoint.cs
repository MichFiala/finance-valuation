using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Delete;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Spendings.Delete;

internal class DeleteSpendingsEndpoint(DeleteHandler deleteHandler, SpendingRepository spendingRepository) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/spendings/{id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Deletes a spending";
            s.Description = "Deletes a spending entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");

        return await deleteHandler.HandleAsync(id, spendingRepository, HttpContext);
    }
}