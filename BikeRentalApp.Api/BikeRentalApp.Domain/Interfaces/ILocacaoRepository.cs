using BikeRentalApp.Domain.Entities;

namespace BikeRentalApp.Domain.Interfaces {
    public interface ILocacaoRepository {
        Task<Locacao> CreateAsync(Locacao locacao);
        Task<Locacao?> GetByIdAsync(string identificador);
        Task UpdateAsync(Locacao locacao);
    }
}
