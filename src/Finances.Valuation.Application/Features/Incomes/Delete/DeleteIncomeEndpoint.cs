using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Delete;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Incomes.Delete;

internal class DeleteIncomeEndpoint(DeleteHandler deleteHandler, IncomeRepository incomeRepository) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/incomes/{id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Deletes a income";
            s.Description = "Deletes a income entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");

        return await deleteHandler.HandleAsync(id, incomeRepository, HttpContext);
    }
}