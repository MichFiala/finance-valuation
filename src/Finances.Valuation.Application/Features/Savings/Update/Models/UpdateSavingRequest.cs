namespace Finances.Valuation.Application.Features.Savings.Update.Models;

    public class UpdateSavingRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Amount { get; set; }
    }
