using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Infrastructure.Data.Models
{
    public class Article
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(Const.TitleMaxLength)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(Const.ContentMaxLength)]
        public string? Content { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
