using FastEndpoints;
using Finances.Valuation.Application.Features.Investments.Models;
using Finances.Valuation.Application.Features.Shared.Endpoints.Update;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Investments.Update;

internal class UpdateInvestmentEndpoint(UpdateHandler updateHandler, InvestmentRepository investmentRepository) : Endpoint<InvestmentDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/investments/{Id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Updates an existing investment entry";
            s.Description = "Updates an existing investment entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(InvestmentDto request, CancellationToken ct)
    {
        return await updateHandler.HandleAsync(request, (investment) =>
        {
            investment.Name = request.Name;
            investment.Amount = request.Amount;
        }, investmentRepository, HttpContext);
    }
}