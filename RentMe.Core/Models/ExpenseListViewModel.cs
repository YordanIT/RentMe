namespace RentMe.Core.Models
{
    public class ExpenseListViewModel
    {
        public string Id { get; init; }
        public decimal Rent { get; set; }
        public decimal EntranceFee { get; set; }
        public decimal Heating { get; set; }
        public decimal Water { get; set; }
        public decimal Electricity { get; set; }
        public decimal? Other { get; set; }
        public bool IsPaid { get; set; }
        public string? Comment { get; set; }
        public string? PropertyId { get; set; }
    }
}
