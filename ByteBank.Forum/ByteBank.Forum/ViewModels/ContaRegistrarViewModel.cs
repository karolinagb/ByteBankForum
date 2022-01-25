using System.ComponentModel.DataAnnotations;

namespace ByteBank.Forum.ViewModels
{
    public class ContaRegistrarViewModel
    {
        public string UserName { get; set; }

        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; }


        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
