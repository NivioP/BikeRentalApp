using BikeRentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApp.Infrastructure.Data {
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Entregador> Entregadores { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Moto>()
             .HasIndex(m => m.Placa)
             .IsUnique();

            modelBuilder.Entity<Entregador>()
                .HasIndex(e => e.CNPJ)
                .IsUnique();

            modelBuilder.Entity<Entregador>()
                .HasIndex(e => e.Numero_CNH)
                .IsUnique();

            modelBuilder.Entity<Locacao>()
                .HasOne<Entregador>(l => l.Entregador)
                .WithMany(e => e.Locacoes) 
                .HasForeignKey(l => l.Entregador_Id)
                .OnDelete(DeleteBehavior.Restrict); 
          
            modelBuilder.Entity<Locacao>()
                .HasOne<Moto>(l => l.Moto) 
                .WithMany(m => m.Locacoes) 
                .HasForeignKey(l => l.Moto_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
