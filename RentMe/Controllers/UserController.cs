using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentMe.Core.Contracts;
using RentMe.Infrastructure.Data.Identity;

namespace RentMe.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService service;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _service)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            service = _service;
        }

        public IActionResult Users()
        {
            var users = service.GetUsers();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> SetAsAdmin(string id)
        {
           var user = await service.SetAsAdmin(id);

            return Ok(user);
        }

        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
            });

            return Ok();
        }
    }
}
