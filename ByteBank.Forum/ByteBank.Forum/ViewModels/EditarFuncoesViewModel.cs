using System.Collections.Generic;

namespace ByteBank.Forum.ViewModels
{
    public class EditarFuncoesViewModel
    {
        public UsuarioViewModel Usuario { get; set; }
        public List<UserRolesViewModel> UserRoles { get; set; } = new List<UserRolesViewModel>();

        public EditarFuncoesViewModel()
        {

        }

        public EditarFuncoesViewModel(UsuarioViewModel usuario, List<UserRolesViewModel> userRoles)
        {
            Usuario = usuario;
            UserRoles.AddRange(userRoles);
        }
    }
}
