using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Core.Models
{
    public class TenantFormModel
    {
        [Required]
        [StringLength(Const.FirstNameMaxLength, MinimumLength = Const.FirstNameMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(Const.LastNameMaxLength, MinimumLength = Const.LastNameMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}
