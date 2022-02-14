using System.ComponentModel.DataAnnotations;

namespace ByteBank.Forum.ViewModels
{
    public class ContaMinhaContaViewModel
    {
        [Required]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Número de celular")]
        public string NumeroCelular { get; set; }

        [Display(Name = "Deseja habilitar autenticação de dois fatores?")]
        public bool HabilitarAutenticacaoDoisFatores { get; set; }

        public bool NumeroCelularConfirmado { get; set; }
    }
}
