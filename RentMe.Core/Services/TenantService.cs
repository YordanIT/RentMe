using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;

namespace RentMe.Core.Services
{
    public class TenantService : ITenantService
    {
        private readonly IApplicationDbRepository repo;

        public TenantService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public TenantViewModel GetTenant(PropertyListViewModel model)
        {
            var property = repo.All<Property>()
                .Where(p => p.IsDeleted == false)
                .Where(p => p.Id == model.Id)
                .FirstOrDefault();

            var tenant = repo.All<Tenant>()
                .Where(t => t.PropertyId == property.Id)
                .Where(t => t.IsDeleted == false)
                .Select(t => new TenantViewModel
                {
                    Id = t.Id.ToString(),
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Email = t.Email,
                    Phone = t.Phone
                }).FirstOrDefault();

            return tenant;
        }

        public async Task AddTenant(TenantFormModel model)
        {
            var proprerty = await repo.All<Property>().FirstAsync(p => p.Address == model.Address);
            var tenant = await repo.All<Tenant>().FirstOrDefaultAsync(t => t.Email == model.Email);

            if (tenant != null)
            {
                throw new ArgumentException("Tenant with this email already exists!");
            }

            tenant = new Tenant
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Property = proprerty
            }; 

            await repo.AddAsync(tenant);
            await repo.SaveChangesAsync();
        }

        public async Task RemoveTenant(TenantViewModel model)
        {
            var tenant = await repo.All<Tenant>()
               .Where(t => t.IsDeleted == false)
               .FirstOrDefaultAsync(t => t.Id.ToString() == model.Id);

            if (tenant == null)
            {
                throw new ArgumentException("Tenant does not exist!");
            }

            tenant.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public  IEnumerable<SelectListItem> GetProperties(PropertyListViewModel model)
        {
            var properties = repo.All<Property>()
               .Where(p => p.IsDeleted == false)
               .Where(p => p.Id == model.Id)
               .Select(p => new SelectListItem
               {
                   Text = model.Address,
               }).ToList();

            return properties;
        }
    }
}
