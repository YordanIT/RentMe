using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Infrastructure.Data.Models
{
    public class PropertyType
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(Const.PropertyTypeMaxLength)]
        public string? Type { get; set; }
    }
}
