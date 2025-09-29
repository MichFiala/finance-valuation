using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Delete;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Strategies.Delete;

internal class DeleteStrategyEndpoint(DeleteHandler deleteHandler, StrategyRepository strategyRepository): EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/strategies/{id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Deletes a strategy";
            s.Description = "Deletes a strategy entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");

        return await deleteHandler.HandleAsync(id, strategyRepository, HttpContext);
    }
}
