using BikeRentalApp.Domain.Entities;

namespace BikeRentalApp.Domain.Interfaces {
    public interface IEntregadorRepository {
        Task AddAsync(Entregador entregador);

        Task<Entregador?> GetByIdAsync(string id);
        Task<bool> NumeroCNHExistsAsync(string numeroCnh);
    }
}
