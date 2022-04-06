namespace RentMe.Core.Models
{
	public class PropertyListViewModel
	{
        public Guid Id { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public int Floor { get; set; }

        public double Area { get; set; }

        public bool HasParking { get; set; }

        public bool HasElevator { get; set; }

        public bool HasFurniture { get; set; }

        public string? Type { get; set; }
    }
}
