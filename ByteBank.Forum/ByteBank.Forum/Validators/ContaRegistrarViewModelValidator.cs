using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ByteBank.Forum.Validators
{
    public class ContaRegistrarViewModelValidator : AbstractValidator<ContaRegistrarViewModel>
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        public ContaRegistrarViewModelValidator(UserManager<UsuarioAplicacao> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Informe o e-mail")
                .EmailAddress().WithMessage("Digite um e-mail válido (Exemplo: exemplo@exemplo.com)")
                .Must(UniqueEmail).WithMessage("Usuário já existe");
            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Informe a senha")
                .Length(6, 20).WithMessage("Senha deve ter no mínimo 6 e no máximo 20 caractéres")
                .Must(RequireDigit).WithMessage("Senha deve ter pelo menos 1 número")
                .Must(RequiredLowerCase).WithMessage("Senha deve ter pelo menos 1 caracter minúsculo")
                .Must(RequireUppercase).WithMessage("Senha deve ter pelo menos 1 caracter maiúsculo")
                .Must(RequireNonAlphanumeric).WithMessage("Digite pelo menos 1 caracter especial (Exemplo: @ ! & * ...)");
        }

        private bool UniqueEmail(string email)
        {
            if (email != null)
            {
                var user = _userManager.FindByEmailAsync(email);
                if (user.Result == null)
                {
                    return true;
                }
                return false;
            }

            return true;
        }

        private bool RequireDigit(string password)
        {
            if (password != null)
            {
                if (password.Any(x => char.IsDigit(x)))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool RequiredLowerCase(string password)
        {
            if (password != null)
            {
                if (password.Any(x => char.IsLower(x)))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool RequireUppercase(string password)
        {
            if (password != null)
            {
                if (password.Any(x => char.IsUpper(x)))
                {
                    return true;
                }
                return false;
            }
            
            return true;
        }

        private bool RequireNonAlphanumeric(string password)
        {
            if (password != null)
            {
                if (Regex.IsMatch(password, @"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_-]"))
                {
                    return true;
                }
                return false;
            }
           
            return true;
        }
    }
}
