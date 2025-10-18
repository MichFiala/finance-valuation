using System.Text.Json.Serialization;

namespace Finances.Valuation.Application.Features.Strategies.Models;

public class StrategyConfigurationCalculationStepDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? AccountName { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StrategyConfigurationType Type { get; set; }

    public decimal AvailableAmount { get; set; }

    public decimal? MonthlyExpectedContributionAmount { get; set; }

    public decimal? MonthlyExpectedContributionPercentage { get; set; }

    public decimal MonthlyActualContributionAmount { get; set; }

    public decimal MonthlyActualContributionPercentage { get; set; }
    
    public int ReferenceId{ get; set; }
}
