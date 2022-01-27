using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBank.Forum.Controllers
{
    [Authorize]
    public class TopicoController : Controller
    {
        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Criar(TopicoCriarViewModel model)
        {
            return View();
        }
    }
}
