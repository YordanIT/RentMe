using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Identity;
using RentMe.Infrastructure.Data.Models;

namespace RentMe.Core.Contracts
{
    public interface IPropertyService
    {
        IEnumerable<PropertyListViewModel> GetProperties(ApplicationUser user);
        Task AddProperty(PropertyFormModel property, ApplicationUser user);
        Task AddPropertyType(PropertyTypeFormModel propertyType);
        Task DeleteProperty(PropertyListViewModel property);
        Task DeletePropertyType(PropertyTypeViewModel propertyType);
    }
}
