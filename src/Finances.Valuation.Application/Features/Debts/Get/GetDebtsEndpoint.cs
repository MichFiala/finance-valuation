using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Debts.Get.Models;
using Finances.Valuation.Application.Features.Debts.Models;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Debts.Get;

internal class GetDebtsEndpoint(UserManager<User.Models.User> userManager, DebtRepository debtRepository) : Endpoint<EmptyRequest, GetDebtsResponse>
{
    public override void Configure()
    {
        Get("/debts");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets all debts";
            s.Description = "Returns a list of all debt DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");

        IReadOnlyCollection<Debt>? debts = await debtRepository.GetAsync(user.Id);

        var debtDtos = debts.Select(DebtDto.Create).ToList();

        await Send.OkAsync(new GetDebtsResponse
        {
            Debts = debtDtos
        }, ct);
    }
}
