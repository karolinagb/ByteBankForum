using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ByteBank.Forum.Controllers
{
    public class ContaController : Controller
    {
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(ContaRegistrarViewModel model)
        {
            return View();
        }
    }
}
