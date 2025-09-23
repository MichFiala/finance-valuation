using FastEndpoints;
using Finances.Valuation.Application.Features.Investments.Models;
using Finances.Valuation.Application.Features.Shared.Endpoints.Create;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Investments.Create;

internal class CreateInvestmentEndpoint(CreateHandler createHandler, InvestmentRepository investmentRepository) : Endpoint<InvestmentDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/investments");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Creates a new investment entry";
            s.Description = "Creates a new investment entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(InvestmentDto request, CancellationToken ct)
        => await createHandler.HandleAsync(request, Investment.Create, investmentRepository, HttpContext);       
}
