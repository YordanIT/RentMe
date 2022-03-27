using Microsoft.AspNetCore.Identity;
using RentMe.Core.Common;
using RentMe.Core.Contracts;
using RentMe.Core.Models;
using RentMe.Infrastructure.Data.Identity;
using RentMe.Infrastructure.Data.Repositories;

namespace RentMe.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(IApplicationDbRepository _repo,
                  UserManager<ApplicationUser> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        public async Task SetNames(string id, UserFormModel model)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);
            user.FirstName = model.FirstName?.Trim();
            user.LastName = model.LastName?.Trim();

            await repo.SaveChangesAsync();
        }

        public async Task SetAsAdmin(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);
            await userManager.AddToRoleAsync(user, Const.RoleAdmin);
        }

        public async Task SetAsLandlord(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);
            await userManager.RemoveFromRoleAsync(user, Const.RoleAdmin);
        }

        public IEnumerable<UserListViewModel> GetUsers()
        {
            return repo.All<ApplicationUser>()
                .ToList()
                .Select(u => new UserListViewModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    Role = userManager.GetRolesAsync(u).Result.ToList().Any() ?
                    string.Join(", ", userManager.GetRolesAsync(u).Result.ToList())
                    : "Landlord"
                })
                .ToList();
        }
    }
}