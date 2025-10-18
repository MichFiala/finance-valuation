namespace Finances.Valuation.Application.Features.SavingsForecast.Get.Models;

public class GetSavingsForecastResponse
{
    public int Months { get; set; }

    public DateOnly Date { get; set; }
}
