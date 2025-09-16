using Finances.Valuation.Application.Features.Spendings.Models;

namespace Finances.Valuation.Application.Features.Spendings.Get.Models;

public class GetSpendingsResponse
{
    public IReadOnlyCollection<SpendingDto> Spendings { get; set; } = [];
}
