using ByteBank.Forum.Data;
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

            string[] roleNames = { RolesNames.ADMIN, RolesNames.MODERADOR };

            foreach (var role in roleNames)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

            }
        }
    }
}
