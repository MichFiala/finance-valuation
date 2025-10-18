namespace Finances.Valuation.Application.Features.SavingsForecast.Get.Models;

public class GetSavingsForecastRequest
{
    public int SavingId { get; set; }

    public int MainIncomeStrategyId { get; set; }

    public int SideIncomeStrategyId { get; set; }
}
