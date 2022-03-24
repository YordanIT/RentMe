using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentMe.Core.Contracts;

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

        //to do : not working
        [HttpPost]
        public async Task<IActionResult> SetAsAdmin(object id)
        {
           var user = service.SetAsAdmin(id);

            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> CreateRole()
        {
            //await roleManager.CreateAsync(new IdentityRole()
            //{
            //    //Name = "Admin"
            //    Name = "Landlord"
            //});

            return RedirectToAction(nameof(Users));
        }
    }
}
