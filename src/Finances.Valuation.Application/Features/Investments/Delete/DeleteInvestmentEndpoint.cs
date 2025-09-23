using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Delete;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Investments.Delete;

internal class DeleteInvestmentEndpoint(DeleteHandler deleteHandler, InvestmentRepository investmentRepository) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/investments/{id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Deletes a investment";
            s.Description = "Deletes a investment entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");

        return await deleteHandler.HandleAsync(id, investmentRepository, HttpContext);
    }
}