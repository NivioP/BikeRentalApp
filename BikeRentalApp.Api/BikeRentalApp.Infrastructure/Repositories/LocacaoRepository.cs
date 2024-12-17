using BikeRentalApp.Domain.Entities;
using BikeRentalApp.Domain.Interfaces;
using BikeRentalApp.Infrastructure.Data;

namespace BikeRentalApp.Infrastructure.Repositories {
    public class LocacaoRepository: ILocacaoRepository {

        private readonly AppDbContext _context;
        public LocacaoRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<Locacao> CreateAsync(Locacao locacao) {
            await _context.Locacoes.AddAsync(locacao);
            await _context.SaveChangesAsync();
            return locacao;
        }

        public async Task<Locacao?> GetByIdAsync(string identificador) {
            return await _context.Locacoes.FindAsync(identificador);
        }

        public async Task UpdateAsync(Locacao locacao) {
            _context.Locacoes.Update(locacao);
            await _context.SaveChangesAsync();
        }
    }
}
