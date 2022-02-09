using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Forum.Services
{
    public class UsuarioService
    {
        public async Task<List<UserRolesViewModel>> ListarRolesUsers(UsuarioAplicacao usuario, RoleManager<IdentityRole> roleManager, UserManager<UsuarioAplicacao> userManager)
        {
            var roles = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    Id = role.Id,
                    Nome = role.Name
                };

                var result = await userManager.IsInRoleAsync(usuario, role.Name);

                //Verifica se o usuário é membro da role especificada
                if (result == true)
                {
                    userRolesViewModel.IsSelecionado = true;
                }
                else
                {
                    userRolesViewModel.IsSelecionado = false;
                }

                roles.Add(userRolesViewModel);
            }

            return roles;
        }

        public List<UsuarioViewModel> ListarUsuarios(UserManager<UsuarioAplicacao> userManager)
        {

            var usuarios = userManager.Users.Select(x => new UsuarioViewModel(x)).ToList();

            return usuarios;
        }
    }
}
