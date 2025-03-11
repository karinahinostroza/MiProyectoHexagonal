using Microsoft.EntityFrameworkCore;
using Domain.Entities; // 💡 Asegúrate de que existe esta referencia

namespace Infrastructure.Persistence
{
   public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } // 💡 Asegúrate de que Product está en Domain.Entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Datos iniciales (opcional)
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1200.99m },
                new Product { Id = 2, Name = "Mouse", Price = 25.50m }
            );
        }
    }
}
