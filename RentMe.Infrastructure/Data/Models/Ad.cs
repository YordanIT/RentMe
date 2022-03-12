using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Infrastructure.Data.Models
{
    public class Ad
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        public bool ForRent { get; set; }

        [MaxLength(Const.DescriptionMaxLength)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        [ForeignKey(nameof(Property))]
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
