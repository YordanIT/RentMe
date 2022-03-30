using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Identity;
using RentMe.Infrastructure.Data.Models;
using RentMe.Infrastructure.Data.Repositories;

namespace RentMe.Core.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IApplicationDbRepository repo;

        public PropertyService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddProperty(PropertyFormModel model, ApplicationUser user)
        {
            var property = await repo.All<Property>()
                .Where(p => p.IsDeleted == false)
                .Where(p => p.City == model.City)
                .FirstOrDefaultAsync(p => p.Address == model.Address);

            if (property != null)
            {
                throw new ArgumentException("Property with this address already exists!");
            }

            PropertyType propertyType = await repo.All<PropertyType>().FirstAsync(pt => pt.Type == model.Type);

            property = new Property
            {
                TypeId = propertyType.Id,
                Address = model.Address,
                Area = model.Area,
                City = model.City,
                Floor = model.Floor,
                HasParking = model.HasParking,
                HasElevator = model.HasElevator,
                HasFurniture = model.HasFurniture,
                ApplicationUserId = user.Id
            };

            user.Properties.Add(property);

            await repo.AddAsync(property);
            await repo.SaveChangesAsync();
        }

        public async Task AddPropertyType(PropertyTypeFormModel model)
        {
            var propertyType = await repo.All<PropertyType>()
                   .FirstOrDefaultAsync(p => p.Type == model.Type);

            if (propertyType != null)
            {
                throw new ArgumentException("PropertyType already exists!");
            }

            propertyType = new PropertyType
            {
                Type = model.Type
            };

            await repo.AddAsync(propertyType);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteProperty(PropertyListViewModel model)
        {
            var property = await repo.All<Property>()
               .Where(p => p.IsDeleted == false)
               .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (property == null)
            {
                throw new ArgumentException("Property does not exist!");
            }

            property.IsDeleted = true;

            await repo.SaveChangesAsync();
        }

        public IEnumerable<PropertyListViewModel> GetProperties(ApplicationUser user)
        {
            var properties = repo.All<Property>()
                .Where(p => p.IsDeleted == false)
                .Where(p => p.ApplicationUserId == user.Id)
                .ToList()
                .Select(p => new PropertyListViewModel
                {
                    Id = p.Id,
                    Type = repo.All<PropertyType>().First(t => t.Id == p.TypeId).Type,
                    Address = p.Address,
                    Area = p.Area,
                    City = p.City,
                    Floor = p.Floor,
                    HasParking = p.HasParking,
                    HasElevator = p.HasElevator,
                    HasFurniture = p.HasFurniture
                })
                .OrderByDescending(p => p.Area)
                .ToList();

            return properties;
        }

        public IEnumerable<SelectListItem> GetPropertyTypes()
        {
            var propertyTypes = repo.All<PropertyType>()
                .Select(pt => new SelectListItem
                {
                    Text = pt.Type
                }).ToList();

            return propertyTypes;
        }
    }
}
