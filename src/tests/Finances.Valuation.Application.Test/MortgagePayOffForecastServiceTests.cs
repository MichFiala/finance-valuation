using Finances.Valuation.Application.Features.Debts;

namespace Finances.Valuation.Application.Test;

[TestFixture]
public class MortgagePayOffForecastServiceTests
{
    [Test]
    public void ForecastPayOffMonths_WithSavings_ReturnsExpectedMonths()
    {
        var service = new MortgagePayOffForecastService();

        var debt = new Features.Debts.Models.Debt
        {
            UserId = string.Empty,
            Name = "Mortgage",
            Amount = 2_787_377.81M,
            Interest = 5.19M,
            Payment = 15_159M,
            Saving = new Features.Savings.Models.Saving
            {
                UserId = string.Empty,
                Name = "Offset saving",
                Amount = 783_092M,
                ExpectedMonthlyContributionAmount = 0M
            }
        };

        int result = service.ForecastPayOffMonths(debt);

        Assert.That(result, Is.GreaterThan(0).And.LessThan(360));
        Assert.That(result, Is.EqualTo(197));
    }
        
        [Test]
    public void ForecastPayOffMonths_WithSavings_With_Contribution_ReturnsExpectedMonths()
    {
        var service = new MortgagePayOffForecastService();

        var debt = new Features.Debts.Models.Debt
        {
            UserId = string.Empty,
            Name = "Mortgage",
            Amount = 2_787_377.81M,
            Interest = 5.19M,
            Payment = 15_159M,
            Saving = new Features.Savings.Models.Saving
            {
                UserId = string.Empty,
                Name = "Offset saving",
                Amount = 783_092M,
                ExpectedMonthlyContributionAmount = 27_500M
            }
        };

        int result = service.ForecastPayOffMonths(debt);

        Assert.That(result, Is.GreaterThan(0).And.LessThan(360));
        Assert.That(result, Is.EqualTo(53));
    }
}