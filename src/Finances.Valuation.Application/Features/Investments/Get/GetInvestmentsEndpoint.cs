using System.Security.Authentication;
using FastEndpoints;
using Finances.Valuation.Application.Features.Investments.Get.Models;
using Finances.Valuation.Application.Features.Investments.Models;
using Finances.Valuation.Application.Features.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Finances.Valuation.Application.Features.Investments.Get;

internal class GetInvestmentsEndpoint(UserManager<User.Models.User> userManager, InvestmentRepository investmentRepository) : Endpoint<EmptyRequest, GetInvestmentsResponse>
{
    public override void Configure()
    {
        Get("/investments");
        AuthSchemes(IdentityConstants.ApplicationScheme);
        Summary(s =>
        {
            s.Summary = "Gets all investments";
            s.Description = "Returns a list of all investment DTOs";
        });
    }

    public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
    {
        User.Models.User? user = await userManager.FindByEmailAsync(HttpContext.Email()) ?? throw new AuthenticationException($"User not found by email {HttpContext.Email()}");        

        IReadOnlyCollection<Investment>? investments = await investmentRepository.GetAsync(user.Id);

        var investmentDtos = investments.Select(InvestmentDto.Create).ToList();

        await Send.OkAsync(new GetInvestmentsResponse
        {
            Investments = investmentDtos
        }, ct);
    }
}
