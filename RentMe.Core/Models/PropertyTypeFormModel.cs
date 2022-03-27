using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Core.Models
{
    public class PropertyTypeFormModel
    {
        [Required]
        [StringLength(Const.PropertyTypeMaxLength, MinimumLength = Const.PropertyTypeMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? Type { get; set; }
    }
}
