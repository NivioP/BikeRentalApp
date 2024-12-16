using BikeRentalApp.Domain.Entities;
using BikeRentalApp.Domain.Interfaces;
using BikeRentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApp.Infrastructure.Repositories {
    public class EntregadorRepository : IEntregadorRepository {
        private readonly AppDbContext _context;

        public EntregadorRepository(AppDbContext context) {
            _context = context;
        }

        public async Task AddAsync(Entregador entregador) {
            await _context.Entregadores.AddAsync(entregador);
            await _context.SaveChangesAsync();
        }

        public async Task<Entregador?> GetByIdAsync(string id) {
            return await _context.Entregadores.FindAsync(id);
        }

        public async Task<bool> NumeroCNHExistsAsync(string numeroCnh) {
            return await _context.Entregadores
                .AnyAsync(e => e.Numero_CNH == numeroCnh);
        }

    }
}
