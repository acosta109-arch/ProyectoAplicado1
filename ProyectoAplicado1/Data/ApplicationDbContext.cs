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
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<DetalleItems> DetalleItems { get; set; }

        public DbSet<OrdenesDelivery> OrdenesDelivery { get; set; }
    }
}
          