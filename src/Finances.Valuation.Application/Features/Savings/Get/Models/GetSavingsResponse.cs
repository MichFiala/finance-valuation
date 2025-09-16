using Finances.Valuation.Application.Features.Savings.Models;

namespace Finances.Valuation.Application.Features.Savings.Get.Models;

public class GetSavingsResponse
{
    public IReadOnlyList<SavingDto> Savings { get; set; } = [];
}
