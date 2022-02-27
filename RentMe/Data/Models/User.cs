using RentMe.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

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

        public bool IsTenant { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
