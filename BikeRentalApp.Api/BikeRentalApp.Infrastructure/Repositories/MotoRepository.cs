using BikeRentalApp.Domain.Entities;
using BikeRentalApp.Domain.Interfaces;
using BikeRentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalApp.Infrastructure.Repositories {
    public class MotoRepository : IMotoRepository {

        private readonly AppDbContext _context;

        public MotoRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Moto>> GetAllAsync() {
            return await _context.Motos.ToListAsync();
        }

        public async Task<Moto?> GetByIdAsync(string id) {
            return await _context.Motos.FindAsync(id);
        }

        public async Task AddAsync(Moto moto) {
            await _context.Motos.AddAsync(moto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Moto moto) {
            _context.Motos.Update(moto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id) {
            var moto = await GetByIdAsync(id);
            if (moto != null) {
                _context.Motos.Remove(moto);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> PlacaExistsAsync(string placa) {
            return await _context.Motos
                .AnyAsync(m => m.Placa == placa);
        }
    }
}
