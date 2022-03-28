using RentMe.Infrastructure.Data.Common;
using RentMe.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Infrastructure.Data.Models
{
    public class Property
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [MaxLength(Const.CityMaxLength)]
        public string? City { get; set; }

        [Required]
        [MaxLength(Const.AddressMaxLength)]
        public string? Address { get; set; }

        public int Floor { get; set; }

        [Range(Const.AreaMinLength, Const.AreaMaxLength)]
        public double Area { get; set; }

        public bool HasParking { get; set; }

        public bool HasElevator { get; set; }

        public bool HasFurniture { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        [ForeignKey(nameof(PropertyType))]
        public int TypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
