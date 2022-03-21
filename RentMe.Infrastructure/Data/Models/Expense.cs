using RentMe.Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Infrastructure.Data.Models
{
    public class Expense
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();
        public decimal Rent { get; set; }
        public decimal EntranceFee { get; set; }
        public decimal Heating { get; set; }
        public decimal Water { get; set; }
        public decimal Electricity { get; set; }
        public decimal? Other { get; set; }
        public bool IsPaid { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        [MaxLength(Const.CommentMaxLength)]
        public string? Comment { get; set; }

        [Required]
        [ForeignKey(nameof(Property))]

        public Guid PropertyId { get; set; }
        public Property? Property { get; set; }

    }
}
