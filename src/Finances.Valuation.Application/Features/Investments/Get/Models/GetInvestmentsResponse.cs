
using Finances.Valuation.Application.Features.Investments.Models;

namespace Finances.Valuation.Application.Features.Investments.Get.Models;

public class GetInvestmentsResponse
{
    public IReadOnlyList<InvestmentDto> Investments { get; set; } = [];
}
