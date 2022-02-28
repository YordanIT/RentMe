using RentMe.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Data.Models
{
    public class Advertisement
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public bool ForRent { get; set; }

        [MaxLength(Const.DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [ForeignKey(nameof(Property))]
        public string? PropertyId { get; set; }
        public Property? Property { get; set; }

        [ForeignKey(nameof(Image))]
        public int ImageId { get; set; }
        public Image? Image { get; set; }
    }
}
