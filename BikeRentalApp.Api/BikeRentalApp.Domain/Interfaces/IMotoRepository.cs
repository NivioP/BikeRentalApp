using BikeRentalApp.Domain.Entities;

namespace BikeRentalApp.Domain.Interfaces {
    public interface IMotoRepository {
        Task AddAsync(Moto moto);
        Task<IEnumerable<Moto>> GetAllAsync();
        Task UpdateAsync(Moto moto);
        Task<Moto?> GetByIdAsync(string id);       
        Task DeleteAsync(string id);
        Task<bool> PlacaExistsAsync(string placa); 
    }
}
