using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Core.Models
{
    public class ImageFormModel
    {
    //    [StringLength(Const.TitleMaxLength, MinimumLength = Const.TitleMinLength,
    //        ErrorMessage = "{0} must be between {2} and {1} symbols")]
    //    public string? Title { get; set; }

        [StringLength(Const.DescriptionMaxLength, MinimumLength = Const.DescriptionMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? Description { get; set; }

        //public byte[]? Data { get; set; }
    }
}
