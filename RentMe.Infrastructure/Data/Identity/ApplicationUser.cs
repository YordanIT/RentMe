using Microsoft.AspNetCore.Identity;
using RentMe.Infrastructure.Data.Common;
using RentMe.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace RentMe.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(Const.FirstNameMaxLength)]
        public string? FirstName { get; set; }

        [MaxLength(Const.LastNameMaxLength)]
        public string? LastName { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
