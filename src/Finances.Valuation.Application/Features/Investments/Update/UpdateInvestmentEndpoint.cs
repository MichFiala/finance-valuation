using FastEndpoints;
using Finances.Valuation.Application.Features.Investments.Models;
using Finances.Valuation.Application.Features.Investments.Update.Models;

namespace Finances.Valuation.Application.Features.Investments.Update;

internal class UpdateInvestmentEndpoint(InvestmentRepository investmentRepository) : Endpoint<UpdateInvestmentRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/investments/{Id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Updates an existing investment entry";
            s.Description = "Updates an existing investment entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(UpdateInvestmentRequest request, CancellationToken ct)
    {
        if (request is null)
        {
            throw new ArgumentException("UpdateInvestmentRequest cannot be null", nameof(request));
        }
        Investment investment = await investmentRepository.GetAsync(request.Id) ?? throw new ArgumentException($"Investment with id {Route<int>("id")} not found", nameof(request));

        investment.Name = request.Name;
        investment.Amount = request.Amount;

        await investmentRepository.SaveAsync(investment);

        return new EmptyResponse();
    }
}