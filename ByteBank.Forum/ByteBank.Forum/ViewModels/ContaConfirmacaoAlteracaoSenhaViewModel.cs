using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ByteBank.Forum.ViewModels
{
    public class ContaConfirmacaoAlteracaoSenhaViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NovaSenha { get; set; }
    }
}
