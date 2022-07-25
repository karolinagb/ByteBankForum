using Microsoft.AspNetCore.Identity;

namespace ByteBank.Forum.Models
{
    public class UsuarioAplicacao : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}
