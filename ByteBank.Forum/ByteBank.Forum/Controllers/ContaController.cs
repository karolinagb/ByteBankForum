using ByteBank.Forum.Models;
using ByteBank.Forum.Services;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace ByteBank.Forum.Controllers
{

    public class ContaController : Controller
    {
        //UserManager é usado para manipulação do dado do usuário //Operações de criar usuario, deletar e etc
        private readonly UserManager<UsuarioAplicacao> _userManager;
        //private readonly SignInManager<UsuarioAplicacao> _signInManager;
        private readonly EmailService _emailService;
        //SignInManager cuida das operações de login, logout etc
        private readonly SignInManager<UsuarioAplicacao> _signInManager;

        public ContaController(UserManager<UsuarioAplicacao> userManager, SignInManager<UsuarioAplicacao> signInManager, EmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
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

        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(ContaLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(
                            user.UserName,
                            model.Senha,
                            isPersistent: model.ContinuarLogado, //Se o usuario deve continua logado ou não
                            lockoutOnFailure: false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Credenciais inválidas");
                    return View();
                }
                ModelState.AddModelError("", "Credenciais inválidas");
                return View();
            }
            //Algo de errado aconteceu
            return View();
        }

        public async Task<ActionResult> ConfirmacaoEmail(string userId, string code)
        {
            if (userId == null || code == null)
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

        [HttpPost]
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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