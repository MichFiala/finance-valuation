using FastEndpoints;
using Finances.Valuation.Application.Features.Savings.Models;

namespace Finances.Valuation.Application.Features.Savings.Create;

internal class CreateSavingEndpoint(SavingRepository savingRepository) : Endpoint<SavingDto, SavingDto>
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

    public override async Task<SavingDto> HandleAsync(SavingDto savingDto, CancellationToken ct)
    {
        var saving = new Saving
        {
            Name = savingDto.Name,
            Amount = savingDto.Amount,
            TargetAmount = savingDto.TargetAmount,
            ExpectedMonthlyContributionAmount = savingDto.ExpectedMonthlyContributionAmount
        };

        await savingRepository.SaveAsync(saving);

        return SavingDto.Create(saving);
    }
}
