using FastEndpoints;
using Finances.Valuation.Application.Features.Spendings.Models;
using Finances.Valuation.Application.Features.Spendings.Update.Models;

namespace Finances.Valuation.Application.Features.Spendings.Update;

internal class UpdateSpendingEndpoint(SpendingRepository spendingRepository) : Endpoint<UpdateSpendingRequest, EmptyResponse>    
{
    public override void Configure()
    {
        Post("/spendings/{Id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Updates an existing spending entry";
            s.Description = "Updates an existing spending entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(UpdateSpendingRequest request, CancellationToken ct)
    {
        if (request is null)
        {
            throw new ArgumentException("UpdateSpendingRequest cannot be null", nameof(request));
        }
        Spending spending = await spendingRepository.GetAsync(request.Id) ?? throw new ArgumentException($"Spending with id {Route<int>("id")} not found", nameof(request));

        spending.Name = request.Name;
        spending.Amount = request.Amount;
        spending.Frequency = request.Frequency;
        spending.IsMandatory = request.IsMandatory;

        await spendingRepository.SaveAsync(spending);

        return new EmptyResponse();
    }
}
