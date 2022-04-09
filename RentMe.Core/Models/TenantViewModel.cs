namespace RentMe.Core.Models
{
    public class TenantViewModel
    {
        public Guid? Id { get; init; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

    }
}
