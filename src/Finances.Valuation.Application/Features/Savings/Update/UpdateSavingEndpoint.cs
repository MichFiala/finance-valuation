using FastEndpoints;
using Finances.Valuation.Application.Features.Savings.Models;
using Finances.Valuation.Application.Features.Savings.Update.Models;

namespace Finances.Valuation.Application.Features.Savings.Update;

internal class UpdateSavingEndpoint(SavingRepository savingRepository) : Endpoint<UpdateSavingRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/savings/{Id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Updates an existing saving entry";
            s.Description = "Updates an existing saving entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(UpdateSavingRequest request, CancellationToken ct)
    {
        if (request is null)
        {
            throw new ArgumentException("UpdateSavingRequest cannot be null", nameof(request));
        }
        Saving saving = await savingRepository.GetAsync(request.Id) ?? throw new ArgumentException($"Saving with id {Route<int>("id")} not found", nameof(request));

        saving.Name = request.Name;
        saving.Amount = request.Amount;

        await savingRepository.SaveAsync(saving);

        return new EmptyResponse();
    }
}