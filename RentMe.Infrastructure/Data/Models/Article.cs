using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Data.Models
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

        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Author))]
        public string? AuthorId { get; set; }
        public User? Author { get; set; }
    }
}
