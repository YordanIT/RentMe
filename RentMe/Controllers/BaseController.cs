using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentMe.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
