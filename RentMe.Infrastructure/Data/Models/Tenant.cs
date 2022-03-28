using RentMe.Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Infrastructure.Data.Models
{
    public class Tenant
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [MaxLength(Const.FirstNameMaxLength)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(Const.LastNameMaxLength)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        [ForeignKey(nameof(Property))]
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
