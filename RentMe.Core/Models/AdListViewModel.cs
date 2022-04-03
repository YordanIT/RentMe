namespace RentMe.Core.Models
{
    public class AdListViewModel
    {
        public string Id { get; init; }
                
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }
        
        public string City { get; set; }

        public string Address { get; set; }

        public int Floor { get; set; }

        public double Area { get; set; }

        public string HasParking { get; set; }

        public string HasElevator { get; set; }

        public string HasFurniture { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ICollection<string> Images { get; set; }
    }
}
