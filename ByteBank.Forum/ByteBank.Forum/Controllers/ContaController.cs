using AutoMapper;
using ByteBank.Forum.Models;
using ByteBank.Forum.Services;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ByteBank.Forum.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        //private readonly SignInManager<UsuarioAplicacao> _signInManager;
        private readonly EmailService _emailService;

        public ContaController(UserManager<UsuarioAplicacao> userManager, SignInManager<UsuarioAplicacao> signInManager, 
            IMapper mapper, EmailService emailService)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _emailService = emailService;
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
                var user = new UsuarioAplicacao()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    NomeCompleto = model.NomeCompleto
                };


                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: true);
                    //var _model = _mapper.Map<UsuarioAplicacao>(model);

                    await EnviarEmailDeConfirmacao(user);

                     //Formatar para que esse codigo venha correto e nao tenha nenhuma conversão de caracteres
                     //var encodedCode = HttpUtility.UrlEncode(code);

                    return View("~/Views/Conta/AguardandoConfirmacao.cshtml");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }
        public async Task<ActionResult> ConfirmacaoEmail(string userId, string code)
        {
            if(userId == null || code == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/Shared/Error.cshtml");
        }

        private async Task EnviarEmailDeConfirmacao(UsuarioAplicacao model)
        {
            var code = await _userManager
                  .GenerateEmailConfirmationTokenAsync(model);

            var linkDeCallBack = Url.Action(
                "ConfirmacaoEmail", 
                "Conta", 
                new { userId = model.Id, code = code },
                //Protocolo do link (http ou https) = Deve ser o mesmo da aplicação em si.
                //Vai retornar o protocolo da aplicação
                Request.Scheme
                ); 

            await _emailService.EnviarEmail(new[] { model.Email },
                    "Link de Ativação", linkDeCallBack);
        }
    }
}