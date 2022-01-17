using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ByteBank.Forum.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        private readonly SignInManager<UsuarioAplicacao> _signInManager;

        public ContaController(UserManager<UsuarioAplicacao> userManager, SignInManager<UsuarioAplicacao> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(ContaRegistrarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    user = new UsuarioAplicacao()
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        NomeCompleto = model.NomeCompleto
                    };
                }

                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}
