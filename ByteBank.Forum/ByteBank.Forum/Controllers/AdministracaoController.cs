using ByteBank.Forum.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBank.Forum.Controllers
{
    [Authorize(Roles = RolesNames.ADMIN)]
    public class AdministracaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
