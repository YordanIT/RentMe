using Microsoft.AspNetCore.Mvc.Rendering;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Identity;

namespace RentMe.Core.Contracts
{
    public interface IPropertyService
    {
        IEnumerable<PropertyListViewModel> GetProperties(ApplicationUser user);
        IEnumerable<SelectListItem> GetPropertyTypes();
        Task AddProperty(PropertyFormModel property, ApplicationUser user);
        Task AddPropertyType(PropertyTypeFormModel propertyType);
        Task DeleteProperty(PropertyListViewModel property);
    }
}
