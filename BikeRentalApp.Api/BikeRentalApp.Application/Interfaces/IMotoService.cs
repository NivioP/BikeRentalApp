using BikeRentalApp.Application.DTOs;

namespace BikeRentalApp.Application.Interfaces {
    public interface IMotoService {
        Task<MotoDto> CreateAsync(CreateMotoDto createDto);
        Task<IEnumerable<MotoDto>> GetAllAsync();
        Task<MotoDto> GetByIdAsync(string id);
        Task UpdateAsync(string id, UpdateMotoPlacaDto updateDto);
        Task DeleteAsync(string id);
    }
}
