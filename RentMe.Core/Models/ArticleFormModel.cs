using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Core.Models
{
    public class ArticleFormModel
    {
        [Required]
        [StringLength(Const.TitleMaxLength, MinimumLength = Const.TitleMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? Title { get; set; }

        [Required]
        [StringLength(Const.ContentMaxLength, MinimumLength = Const.ContentMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? Content { get; set; }
    }
}
