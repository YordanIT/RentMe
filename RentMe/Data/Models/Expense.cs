using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Data.Models
{
    public class Expense
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public decimal Rent { get; set; }
        public decimal EntanceFee { get; set; }
        public decimal Heating { get; set; }
        public decimal Water { get; set; }
        public decimal Electricity { get; set; }
        public decimal? Other { get; set; }
        public decimal Total { get; set; }
        public bool IsPaid { get; set; }

        public string? Comment { get; set; }

        [Required]
        [ForeignKey(nameof(Property))]

        public string? PropertyId { get; set; }
        public Property? Property { get; set; }

    }
}
