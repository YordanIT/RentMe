using Microsoft.AspNetCore.Mvc.Rendering;
using RentMe.Core.Models;

namespace RentMe.Core.Contracts
{
    public interface ITenantService
    {
        TenantViewModel GetTenant(PropertyListViewModel property);

        IEnumerable<SelectListItem> GetProperties(PropertyListViewModel property);

        Task AddTenant(TenantFormModel tenant);

        Task RemoveTenant(TenantViewModel tenant);
    }
}
