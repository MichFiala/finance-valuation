
using Finances.Valuation.Application.Features.Debts.Models;

namespace Finances.Valuation.Application.Features.Debts.Get.Models;

public class GetDebtsResponse
{
    public IReadOnlyList<DebtDto> Debts { get; set; } = [];
}
