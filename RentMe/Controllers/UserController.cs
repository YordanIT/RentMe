using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;
using RentMe.Core.Models;

namespace RentMe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService service;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            IUserService _service)
        {
            roleManager = _roleManager;
            service = _service;
        }

        public IActionResult Users()
        {
            var users = service.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> SetAsAdmin(UserListViewModel user)
        {
            await service.SetAsAdmin(user.Id);

            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> SetAsLandlord(UserListViewModel user)
        {
            await service.SetAsLandlord(user.Id);

            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
                //Name = "Landlord"
            });

            return RedirectToAction(nameof(Users));
        }
    }
}
