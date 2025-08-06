using FastEndpoints;
using Finances.Valuation.Application.Features.Investments.Models;

namespace Finances.Valuation.Application.Features.Investments.Create;

internal class CreateInvestmentEndpoint(InvestmentRepository investmentRepository) : Endpoint<List<InvestmentDto>, InvestmentDto>
{
    public override void Configure()
    {
        Post("/investments");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Creates a new investment entry";
            s.Description = "Creates a new investment entry";
        });
    }

    public override async Task<List<InvestmentDto>> HandleAsync(List<InvestmentDto> investmentDtos, CancellationToken ct)
    {
        if (investmentDtos == null || investmentDtos.Count == 0)
        {
            throw new ArgumentException("InvestmentDto list cannot be null or empty", nameof(investmentDtos));
        }

        var investments = new List<Investment>();

        foreach (var investmentDto in investmentDtos)
        {
            var investment = Investment.Create(investmentDto);

            await investmentRepository.SaveAsync(investment);

            investments.Add(investment);
        }

        return investments.Select(InvestmentDto.Create).ToList();
    }
}
