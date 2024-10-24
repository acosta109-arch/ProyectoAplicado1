using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoAplicado.Services;
using ProyectoAplicado1.Components;
using ProyectoAplicado1.Components.Account;
using ProyectoAplicado1.Data;

namespace ProyectoAplicado1
{
    public class Program
    {
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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            // Conectar a SQLite
            var connectionString = builder.Configuration.GetConnectionString("ConStr")
                ?? throw new InvalidOperationException("Connection string 'ConStr' not found.");

            // Registrar DbContext con SQLite
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));  // Usar SQLite

            //La Inyeccion del services CocineroServices
            builder.Services.AddScoped<CocineroServices>();

            //La Inyeccion del services ComidaServices
            builder.Services.AddScoped<ComidaServices>();

            //La Inyeccion del services BebidasServices
            builder.Services.AddScoped<BebidasServices>();

            //La Inyeccion del services PostresServices
            builder.Services.AddScoped<PostresServices>();

            //La Inyeccion del services MeserosServices
            builder.Services.AddScoped<MeserosServices>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()  // Usar el nuevo Contexto con SQLite
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            var app = builder.Build();

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
