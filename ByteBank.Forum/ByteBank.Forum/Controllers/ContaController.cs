using ByteBank.Forum.Models;
using ByteBank.Forum.Services;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    await EnviarEmail(user, token, "ConfirmacaoEmail", "Conta", "Link de Ativação");

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

        public ActionResult Login()
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
                            lockoutOnFailure: true);

                    switch (signInResult.ToString())
                    {
                        case "Succeeded":
                            return RedirectToAction("Index", "Home");

                        case "Lockedout":
                            var isSenhaCorreta = await _userManager.CheckPasswordAsync(user, model.Senha);

                            if (isSenhaCorreta)
                            {
                                ModelState.AddModelError("", "A conta está bloqueada");
                                break;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Credenciais inválidas");
                                break;
                            }

                        case "NotAllowed":
                            return View("~/Views/Conta/AguardandoConfirmacao.cshtml");

                        default:
                            ModelState.AddModelError("", "Credenciais inválidas");
                            break;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Credenciais inválidas");
                    return View();

                }
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

        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EsqueciSenha(ContaEsqueciSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                //if (user == null)
                //{
                //    return View("~/Views/Conta/EmailAlteracaoSenhaEnviado.cshtml");
                //}

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                //Gerar token de reset da senha
                //Gerar a url para o usuario
                //Enviar esse email
                await EnviarEmail(user, token, "ConfirmacaoAlteracaoSenha", "Conta", "Alteração de Senha");

                return View("~/Views/Conta/EmailAlteracaoSenhaEnviado.cshtml");
            }

            return View();
        }

        public ActionResult ConfirmacaoAlteracaoSenha(string userId, string token)
        {
            var model = new ContaConfirmacaoAlteracaoSenhaViewModel
            {
                UserId = userId,
                Token = token
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmacaoAlteracaoSenha(ContaConfirmacaoAlteracaoSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Verifica o token recebido

                //Verifica o id do usuario

                //Mudar a senha do usuário
                var user = await _userManager.FindByIdAsync(model.UserId);

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NovaSenha);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach(var e in ModelState.Values.SelectMany(e => e.Errors))
                {
                    ModelState.AddModelError("", e.ErrorMessage);
                }
                
            }
            return View();
        }

        private async Task EnviarEmail(UsuarioAplicacao model, string token, string action, string controlador, string assunto)
        {
            var linkDeCallBack = Url.Action(
                action,
                controlador,
                new { userId = model.Id, token = token },
                //Protocolo do link (http ou https) = Deve ser o mesmo da aplicação em si.
                //Vai retornar o protocolo da aplicação
                Request.Scheme
                );

            await _emailService.EnviarEmail(new[] { model.Email },
                    assunto, linkDeCallBack);
        }
    }
}