using RentMe.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Data.Models
{
    public class Administrator
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(Const.UsernameMaxLenght)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(Const.HashedPasswordMaxLength)]
        public string? Password { get; set; }
    }
}
