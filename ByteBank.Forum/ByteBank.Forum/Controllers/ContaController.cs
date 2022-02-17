using ByteBank.Forum.Models;
using ByteBank.Forum.Services;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

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

        private readonly SmsService _smsService;

        public ContaController(UserManager<UsuarioAplicacao> userManager, EmailService emailService, SignInManager<UsuarioAplicacao> signInManager, SmsService smsService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _smsService = smsService;
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
                    //var encodedCode = HttpUtility.UrlEncode(token);

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

        [HttpPost]
        public ActionResult RegistrarPorAutenticacaoExterna(string provider, string returnUrl = null)
        {

            var redirectUrl = Url.Action("RegistrarPorAutenticacaoExternaCallback", "Conta", new { returnUrl });

            var propriedades = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(propriedades, provider);
        }

        public async Task<ActionResult> RegistrarPorAutenticacaoExternaCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Erro do provedor externo: {remoteError}");
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                TempData["InfoIsNull"] = $"Falha ao fazer autenticação com {info.LoginProvider} \r\n" +
                    $"Entre em contato com o suporte sistemaidentityalura@gmail.com";

                return RedirectToAction(nameof(Registrar));
            }

            var usuarioExistente = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email.ToLower()));

            if (usuarioExistente != null)
            {
                var userAssociado = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (userAssociado != null)
                {
                    await _signInManager.SignInAsync(usuarioExistente, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }

                //Associa a conta externa com a que está no banco
                var resultAddLogin = await _userManager.AddLoginAsync(usuarioExistente, info);

                //if (resultAddLogin.Errors.Any(e => e.Code == "LoginAlreadyAssociated"))
                //{
                //    await _signInManager.SignInAsync(usuarioExistente, isPersistent: false);
                //    return RedirectToAction("Index", "Home");
                //}

                if (resultAddLogin.Succeeded)
                {
                    //Entro com o usuário do banco (agora associado ao usuario externo)
                    await _signInManager.SignInAsync(usuarioExistente, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var erro in resultAddLogin.Errors)
                {
                    ModelState.AddModelError("", $"{erro.Code} : {erro.Description}");
                    return RedirectToAction(nameof(Registrar));
                }

            }

            //Obter informações do usuário de login do google assim
            var novoUsuario = new UsuarioAplicacao();

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                novoUsuario.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
                novoUsuario.UserName = info.Principal.FindFirstValue(ClaimTypes.Email);
            }

            var result2 = await _userManager.CreateAsync(novoUsuario);

            if (result2.Succeeded)
            {
                result2 = await _userManager.AddLoginAsync(novoUsuario, info);
                if (result2.Succeeded)
                {
                    await _signInManager.SignInAsync(novoUsuario, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["FalhaCriarUsuario"] = $"Falha ao fazer autenticação com {info.LoginProvider} \r\n" +
                   $"Entre em contato com o suporte sistemaidentityalura@gmail.com";

            return View(nameof(Registrar));
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

        public ActionResult LoginPorAutenticacaoExterna(string provider)
        {

            var redirectUrl = Url.Action("LoginPorAutenticacaoExternaCallback", "Conta");

            var propriedades = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(propriedades, provider);
        }

        public async Task<ActionResult> LoginPorAutenticacaoExternaCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                TempData["InfoIsNull"] = $"Falha ao fazer autenticação com {info.LoginProvider} \r\n" +
                    $"Entre em contato com o suporte sistemaidentityalura@gmail.com";

                return RedirectToAction(nameof(Login));
            }

            var usuarioExistente = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email.ToLower()));

            if (usuarioExistente != null)
            {
                await _signInManager.SignInAsync(usuarioExistente, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }

            TempData["CredenciaisInvalidas"] = "Credenciais inválidas";

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<ActionResult> ConfirmacaoEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ConfirmEmailAsync(user, token);

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

                foreach (var e in ModelState.Values.SelectMany(e => e.Errors))
                {
                    ModelState.AddModelError("", e.ErrorMessage);
                }

            }
            return View();
        }

        public async Task<ActionResult> MinhaConta()
        {
            var model = new ContaMinhaContaViewModel();

            var email = User.FindFirstValue(ClaimTypes.Email);

            var usuario = await _userManager.FindByEmailAsync(email);

            model.NomeCompleto = usuario.NomeCompleto;
            model.NumeroCelular = usuario.PhoneNumber;
            model.HabilitarAutenticacaoDoisFatores = usuario.TwoFactorEnabled;
            model.NumeroCelularConfirmado = usuario.PhoneNumberConfirmed;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> MinhaConta(ContaMinhaContaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var usuario = await _userManager.FindByEmailAsync(email);

                usuario.NomeCompleto = model.NomeCompleto;

                if (usuario.PhoneNumber != model.NumeroCelular)
                {
                    usuario.PhoneNumberConfirmed = false;
                }

                usuario.PhoneNumber = model.NumeroCelular;

                if (!usuario.PhoneNumberConfirmed)
                {
                    await EnviarSmsConfirmacaoAsync(usuario);
                }

                var result = await _userManager.UpdateAsync(usuario);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError("", $"{e.Code} : { e.Description}");
                }
            }
            return View();
        }

        public ActionResult VerificacaoCodigoCelular()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> VerificacaoCodigoCelular(string token)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var usuario = await _userManager.FindByEmailAsync(email);

            var result = await _smsService.VerificarCodigo(token, usuario.PhoneNumber);

            if (result.Valid == true)
            {
                usuario.PhoneNumberConfirmed = true;
                await _userManager.UpdateAsync(usuario);
                return RedirectToAction("Index", "Home");
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

        private async Task EnviarSmsConfirmacaoAsync(UsuarioAplicacao usuario)
        {
            //Ele gera token de mudança de celular e não de confirmação
            //var token = await _userManager.GenerateChangePhoneNumberTokenAsync(usuario, usuario.PhoneNumber);

            Message message = new Message("Teste", usuario.PhoneNumber);

            await _smsService.Enviar(message);
        }
    }
}