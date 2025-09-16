namespace Finances.Valuation.Application.Features.Summary.Get.Models;

public class GetSummaryResponse
{
    public decimal TotalInvestments { get; set; }
    public decimal TotalSavings { get; set; }
    public decimal TotalDebts { get; set; }
    public decimal TotalMonthlySpendings { get; set; }
}
