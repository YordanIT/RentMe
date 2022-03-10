using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Data.Models
{
    public class Ad
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        public bool ForRent { get; set; }

        [MaxLength(Const.DescriptionMaxLength)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Property))]
        public string? PropertyId { get; set; }
        public Property? Property { get; set; }

        [ForeignKey(nameof(Image))]
        public int ImageId { get; set; }
        public Image? Image { get; set; }
    }
}
