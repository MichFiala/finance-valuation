using Finances.Valuation.Application.Features.Incomes.Models;

namespace Finances.Valuation.Application.Features.Incomes.Get.Models;

public class GetIncomesResponse
{
    public IReadOnlyCollection<IncomeDto> Incomes { get; set; } = [];
}
