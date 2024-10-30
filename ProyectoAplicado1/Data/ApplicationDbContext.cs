using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoAplicado.Models;
using ProyectoAplicado1.Models;

namespace ProyectoAplicado1.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Bebidas> Bebidas { get; set; }
        public DbSet<Cocineros> Cocineros { get; set; }
        public DbSet<Comidas> Comidas { get; set; }
        public DbSet<Meseros> Meseros { get; set; }
        public DbSet<Postres> Postres { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Direcciones> Direcciones { get; set; }
        public DbSet<MetodoPagos> MetodoPagos { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Tarjetas> Tarjetas { get; set; }

    }
}
          