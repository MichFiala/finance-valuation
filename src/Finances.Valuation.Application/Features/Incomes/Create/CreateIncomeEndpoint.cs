using FastEndpoints;
using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Shared.Endpoints.Create;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Incomes.Create;

internal class CreateIncomeEndpoint(CreateHandler createHandler, IncomeRepository incomeRepository) : Endpoint<IncomeDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/incomes");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Creates a new income entry";
            s.Description = "Creates a new income entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(IncomeDto request, CancellationToken ct)
            => await createHandler.HandleAsync(request, Income.Create, incomeRepository, HttpContext);
}
