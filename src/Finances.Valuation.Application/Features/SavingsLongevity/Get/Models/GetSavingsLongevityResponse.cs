namespace Finances.Valuation.Application.Features.SavingsLongevity.Get.Models;

public class GetSavingsLongevityResponse
{
    public int Months { get; set; }

    public DateOnly Till { get; set; }
}
