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

        public async Task<UserEditViewModel> SetAsAdmin(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);
            await userManager.AddToRoleAsync(user, Const.RoleAdmin);

            return new UserEditViewModel()
            {
                Id = user.Id,
                Role = ""//string.Join(", ", userManager.GetRolesAsync(user).Result.ToArray())
            };
        }

        public IEnumerable<UserListViewModel> GetUsers()
        {
            //to do : it is not working
            return repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    Role = ""//string.Join(", ", userManager.GetRolesAsync(u).Result.ToList())
                })
                .ToList();
        }
    }
}