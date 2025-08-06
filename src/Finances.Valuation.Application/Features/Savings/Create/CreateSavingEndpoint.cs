using FastEndpoints;
using Finances.Valuation.Application.Features.Savings.Models;

namespace Finances.Valuation.Application.Features.Savings.Create;

internal class CreateSavingEndpoint(SavingRepository savingRepository) : Endpoint<List<SavingDto>, List<SavingDto>>
{
    public override void Configure()
    {
        Post("/savings");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Creates a new saving entry";
            s.Description = "Creates a new saving entry";
        });
    }

    public override async Task<List<SavingDto>> HandleAsync(List<SavingDto> savingDtos, CancellationToken ct)
    {
        if (savingDtos == null || savingDtos.Count == 0)
        {
            throw new ArgumentException("SavingDto list cannot be null or empty", nameof(savingDtos));
        }

        var savings = new List<Saving>();

        foreach (var savingDto in savingDtos)
        {
            var saving = Saving.Create(savingDto);
            
            await savingRepository.SaveAsync(saving);
            
            savings.Add(saving);
        }

        return savings.Select(SavingDto.Create).ToList();
    }
}
