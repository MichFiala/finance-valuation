using FastEndpoints;
using Finances.Valuation.Application.Features.Debts.Models;
using Finances.Valuation.Application.Features.Debts.Update.Models;

namespace Finances.Valuation.Application.Features.Debts.Update;
internal class UpdateDebtEndpoint(DebtRepository debtRepository) : Endpoint<UpdateDebtRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/debts/{Id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Updates an existing debt entry";
            s.Description = "Updates an existing debt entry";
        });
    }

    public override async Task<EmptyResponse> HandleAsync(UpdateDebtRequest request, CancellationToken ct)
    {
        if (request is null)
        {
            throw new ArgumentException("UpdateDebtRequest cannot be null", nameof(request));
        }
        Debt debt = await debtRepository.GetAsync(request.Id) ?? throw new ArgumentException($"Debt with id {Route<int>("id")} not found", nameof(request));

        debt.Name = request.Name;
        debt.Amount = request.Amount;

        await debtRepository.SaveAsync(debt);

        return new EmptyResponse();
    }
}