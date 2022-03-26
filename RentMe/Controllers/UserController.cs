using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;
using RentMe.Core.Models;

namespace RentMe.Controllers
{
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
            
        public IActionResult RegisterUserNames(UserFormModel user)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            var users = service.GetUsers();

            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetAsAdmin(UserListViewModel user)
        {
            await service.SetAsAdmin(user.Id);

            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetAsLandlord(UserListViewModel user)
        {
            await service.SetAsLandlord(user.Id);

            return RedirectToAction(nameof(Users));
        }

        [Authorize(Roles = "Admin")]
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
