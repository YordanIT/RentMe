using RentMe.Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Infrastructure.Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; init; }

        [MaxLength(Const.TitleMaxLength)]
        public string? Title { get; set; }

        [MaxLength(Const.DescriptionMaxLength)]
        public string? Description { get; set; }

        public byte[]? Data { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
