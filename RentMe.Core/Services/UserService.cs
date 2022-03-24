using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task SetAsAdmin(object id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);
            await userManager.AddToRoleAsync(user, Const.RoleAdmin);
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