using RentMe.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Core.Models
{
	public class PropertyFormModel
	{
        [Required]
        [StringLength(Const.CityMaxLength, MinimumLength = Const.CityMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? City { get; set; }

        [Required]
        [StringLength(Const.AddressMaxLength, MinimumLength = Const.AddressMinLength,
            ErrorMessage = "{0} must be between {2} and {1} symbols")]
        public string? Address { get; set; }

        public int Floor { get; set; }

        [Range(Const.AreaMinLength, Const.AreaMaxLength)]
        public double Area { get; set; }

        public bool HasParking { get; set; }

        public bool HasElevator { get; set; }

        public bool HasFurniture { get; set; }

        [Required]
		public string? Type { get; set; }
	}
}
