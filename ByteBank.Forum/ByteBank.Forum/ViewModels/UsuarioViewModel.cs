using ByteBank.Forum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ByteBank.Forum.ViewModels
{
    public class UsuarioViewModel
    {
        [HiddenInput(DisplayValue = true)]
        public string Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(UsuarioAplicacao usuario)
        {
            Id = usuario.Id;
            NomeCompleto = usuario.NomeCompleto;
            Email = usuario.Email;
            UserName = usuario.UserName;
        }
    }
}
