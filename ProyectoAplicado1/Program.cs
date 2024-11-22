using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoAplicado.Services;
using ProyectoAplicado1.Components;
using ProyectoAplicado1.Components.Account;
using ProyectoAplicado1.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using ProyectoAplicado1.Services;
using CurrieTechnologies.Razor.SweetAlert2;

namespace ProyectoAplicado1
{
    public class Program
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Define the roles that are needed
            string[] roleNames = { "Admin", "Cliente" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                // Check if the role already exists
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the role if it does not exist
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Check for the admin user
            var adminEmail = "Admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            // If the admin user exists, add them to the "Admin" role
            if (adminUser != null)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Get all users to assign the "Cliente" role to those without the admin email
            var users = userManager.Users.ToList(); // Note: You may want to use `ToArrayAsync()` for better performance
            foreach (var user in users)
            {
                if (user.Email != adminEmail)
                {
                    // Add user to the "Cliente" role if they are not the admin
                    var isInRole = await userManager.IsInRoleAsync(user, "Cliente");
                    if (!isInRole)
                    {
                        await userManager.AddToRoleAsync(user, "Cliente");
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSweetAlert2();  // Agregar SweetAlert2

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
            builder.Services.AddScoped<CarritoService>();
            builder.Services.AddScoped<ItemsService>();
            builder.Services.AddScoped<OrdenesServices>();
            builder.Services.AddScoped<DetalleItemsServices>();
            builder.Services.AddScoped<OrdenesDeliveryServices>();
            builder.Services.AddScoped<ReporteService>();
            builder.Services.AddScoped<MesaServices>();

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
