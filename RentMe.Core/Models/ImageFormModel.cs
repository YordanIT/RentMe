using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Core.Models
{
    public class ImageFormModel
    {
        [StringLength(Const.DescriptionMaxLength, MinimumLength = Const.DescriptionMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? Description { get; set; }

    }
}
