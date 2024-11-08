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
        public DbSet<OrderItem> OrdenItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación muchos a muchos entre Ordenes y Items
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Orden)
                .WithMany(o => o.OrdenItems)
                .HasForeignKey(oi => oi.OrdenId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany(i => i.OrdenItems)
                .HasForeignKey(oi => oi.ItemId);
        }
    }
}
          