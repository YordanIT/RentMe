using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Infrastructure.Data.Models
{
    public class Tenant
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [MaxLength(Const.UsernameMaxLenght)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(Const.HashedPasswordMaxLength)]
        public string? Password { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? IBAN { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        [ForeignKey(nameof(Expense))]
        public Guid ExpenseId { get; set; }
        public Expense Expense { get; set; }
    }
}
