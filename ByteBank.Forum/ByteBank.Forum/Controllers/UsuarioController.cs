using ByteBank.Forum.ExtensionMethods;
using ByteBank.Forum.Models;
using ByteBank.Forum.Services;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Forum.Controllers
{
    [Authorize(Roles = RolesNames.ADMIN)]
    public class UsuarioController : Controller
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UserManager<UsuarioAplicacao> userManager, RoleManager<IdentityRole> roleManager, UsuarioService usuarioService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            var usuarios = _usuarioService.ListarUsuarios(_userManager);

            return View(usuarios);
        }

        public async Task<ActionResult> EditarFuncoes(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            UsuarioViewModel usuarioViewModel = new UsuarioViewModel(usuario);

            var roles = new List<UserRolesViewModel>();

            roles = await _usuarioService.ListarRolesUsers(usuario, _roleManager, _userManager);

            EditarFuncoesViewModel editarFuncoesViewModel = new EditarFuncoesViewModel(usuarioViewModel, roles);

            return View(editarFuncoesViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditarFuncoes(EditarFuncoesViewModel editarFuncoesViewModel)
        {
            //if (ModelState.IsValid)
            //{
            //    //Recuperar usuário pelo Id
            //    var usuario = await _userManager.FindByIdAsync(user.Id);

            //    //Buscar todas as roles do banco
            //    //Pegar só as roles que o usuario pertence senão na remoção vai dar erro
            //    var userRoles = await _userManager.GetRolesAsync(usuario);

            //    //Remover todas as roles que o usuário for associado (seriam as que estavam selecionada antes da edição)
            //    var result = await _userManager.RemoveFromRolesAsync(usuario, userRoles);

            //    if (result.Succeeded)
            //    {
            //        var rolesSelecionadas = userRolesViewModels.Where(x => x.IsSelecionado == true).Select(x => x.Nome);

            //        var resultAddRole = await _userManager.AddToRolesAsync(usuario, rolesSelecionadas);

            //        if (resultAddRole.Succeeded)
            //        {
            //            RedirectToAction("Index");
            //        }
            //    }
            //}

            return View();
        }
    }
}
