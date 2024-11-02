using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoAplicado.Services;
using ProyectoAplicado1.Components;
using ProyectoAplicado1.Components.Account;
using ProyectoAplicado1.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ProyectoAplicado1
{
    public class Program
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Definir los roles que se necesitan
            string[] roleNames = { "Admin" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                // Verificar si el rol ya existe
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Crear el rol si no existe
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Buscar el usuario por email
            var user = await userManager.FindByEmailAsync("Admin@gmail.com");

            // Si el usuario existe, añadirlo al rol "Admin"
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            // Conectar a SQLite
            var connectionString = builder.Configuration.GetConnectionString("ConStr")
                ?? throw new InvalidOperationException("Connection string 'ConStr' not found.");

            // Registrar DbContext con SQLite
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));  // Usar SQLite

            // La inyección de servicios personalizados
            builder.Services.AddScoped<CocineroServices>();
            builder.Services.AddScoped<ComidaServices>();
            builder.Services.AddScoped<BebidasServices>();
            builder.Services.AddScoped<PostresServices>();
            builder.Services.AddScoped<MeserosServices>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Configuración de Identity para manejar usuarios y roles
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()  // Usar el nuevo Contexto con SQLite
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            var app = builder.Build();

          //  Ejecutar el método para crear roles y asignar al usuario
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                CreateRoles(services).Wait();  // Ejecutar la creación de roles
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
