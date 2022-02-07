using ByteBank.Forum.Data;
using ByteBank.Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ByteBank.Forum.ExtensionMethods
{
    public static class IdentityRoles
    {
        public static IHost CriarRoles(this IHost host)
        {
            // Create a scope to get scoped services.
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var serviceProvider = services.GetRequiredService<IServiceProvider>();
                var configuration = services.GetRequiredService<IConfiguration>();

                CriarRolesAsync(serviceProvider, configuration).Wait();
            }
            return host;
        }

        public static async Task CriarRolesAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<UsuarioAplicacao>>();

            string[] roleNames = { RolesNames.ADMIN, RolesNames.MODERADOR };

            foreach (var role in roleNames)
            {

                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var powerUser = new UsuarioAplicacao
            {
                UserName = configuration.GetSection("UserSettings")["UserName"],
                Email = configuration.GetSection("UserSettings")["UserEmail"],
                EmailConfirmed = true
            };

            var userPassword = configuration.GetSection("UserSettings")["UserPassword"];

            var user = await userManager.FindByEmailAsync(powerUser.Email);

            if (user == null)
            {
                var result = await userManager.CreateAsync(powerUser, userPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(powerUser, RolesNames.ADMIN);
                }
            }
        }
    }
}
