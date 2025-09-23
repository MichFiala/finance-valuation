using FastEndpoints;
using Finances.Valuation.Application.Features.Shared.Endpoints.Delete;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Debts.Delete;
internal class DeleteDebtEndpoint(DeleteHandler deleteHandler, DebtRepository debtRepository) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/debts/{id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Deletes a new debt";
            s.Description = "Deletes a debt entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");

        return await deleteHandler.HandleAsync(id, debtRepository, HttpContext);
    }
}