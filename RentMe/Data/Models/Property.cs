using RentMe.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMe.Data.Models
{
    public class Property
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

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

        [Required]
        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }
        public Type? Type { get; set; }
    }
}
