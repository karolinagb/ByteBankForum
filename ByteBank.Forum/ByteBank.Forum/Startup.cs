using ByteBank.Forum.Data;
using ByteBank.Forum.Models;
using ByteBank.Forum.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Twilio;

namespace ByteBank.Forum
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddIdentity<UsuarioAplicacao, IdentityRole>(option =>
               {
                   option.SignIn.RequireConfirmedEmail = true;
                   option.Lockout.AllowedForNewUsers = true;
                   option.Lockout.MaxFailedAccessAttempts = 3;
                   option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(0);
               }) //Adiciona o sistema Identiy padr�o para os tipos de perfis especificados
              .AddEntityFrameworkStores<ByteBankForumContext>() //Adiciona uma implementa��o do EntityFramework que armazena as informa��es de identidade
              .AddDefaultTokenProviders(); //Inclui os tokens para troca de senha e envio de e-mail

            //Adicionando o serviço de autenticação do google a aplicação
            services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection = Configuration.GetSection("AutenticacaoExterna:AutenticacaoGoogle");
                options.ClientId = googleAuthNSection["Client_Id"];
                options.ClientSecret = googleAuthNSection["Client_Secret"];
            });

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ByteBankForumContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddMvc().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddScoped<EmailService>();
            services.AddScoped<SmsService>();
            services.AddTransient<UsuarioService>();

            var sidConta = Configuration.GetValue<string>("Twilio:SIDConta");
            var tokenConta = Configuration.GetValue<string>("Twilio:TokenConta");
            TwilioClient.Init(sidConta, tokenConta);

            //Configurações para usar o twilio para envio de SMS
            //O Twilio usa a instância singleton
            //var sidConta = Configuration["Twilio: SIDConta"];
            //var tokenConta = Configuration["Twilio:TokenConta"];
            //TwilioClient.Init(sidConta, tokenConta);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area:exists}/{controller=}/{id?}"
                    );
            });
        }
    }
}
