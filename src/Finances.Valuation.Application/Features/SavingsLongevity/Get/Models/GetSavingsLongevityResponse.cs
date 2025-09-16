using System.Text.Json.Serialization;

namespace Finances.Valuation.Application.Features.SavingsLongevity.Get.Models;

public class GetSavingsLongevityResponse
{
    public int Months { get; set; }

    public DateOnly Till { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SavingsLongevityGrade Grade { get; set; }
}

public enum SavingsLongevityGrade
{
    Critical,
    Insufficient,
    NeedsImprovement,
    Recommended,
    Strong,
    Excellent
}
