using BikeRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApp.Infrastructure.Data {
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
        public DbSet<Moto> Motos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Moto>()
             .HasIndex(m => m.Placa)
             .IsUnique();
        }
    }
}
