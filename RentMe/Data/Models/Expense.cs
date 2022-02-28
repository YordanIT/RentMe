using RentMe.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Data.Models
{
    public class Expense
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public decimal Rent { get; set; }
        public decimal EntranceFee { get; set; }
        public decimal Heating { get; set; }
        public decimal Water { get; set; }
        public decimal Electricity { get; set; }
        public decimal? Other { get; set; }
        public decimal Total
            => Rent + EntranceFee + Heating + Water + Electricity + Other??0; 
        public bool IsPaid { get; set; }

        [MaxLength(Const.CommentMaxLength)]
        public string? Comment { get; set; }

        [Required]
        [ForeignKey(nameof(Property))]

        public string? PropertyId { get; set; }
        public Property? Property { get; set; }

    }
}
