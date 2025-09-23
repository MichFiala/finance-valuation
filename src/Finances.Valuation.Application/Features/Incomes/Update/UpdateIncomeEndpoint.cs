using FastEndpoints;
using Finances.Valuation.Application.Features.Incomes.Models;
using Finances.Valuation.Application.Features.Shared.Endpoints.Update;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Incomes.Update;

internal class UpdateIncomeEndpoint(UpdateHandler updateHandler, IncomeRepository incomeRepository) : Endpoint<IncomeDto, EmptyResponse>
{
    public override void Configure()
    {
        Post("/incomes/{Id}");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Updates an existing income entry";
            s.Description = "Updates an existing income entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(IncomeDto request, CancellationToken ct)
    {
        return await updateHandler.HandleAsync(request, (income) =>
        {
            income.Name = request.Name;
            income.Amount = request.Amount;
            income.Date = request.Date;
        }, incomeRepository, HttpContext);
    }    
}