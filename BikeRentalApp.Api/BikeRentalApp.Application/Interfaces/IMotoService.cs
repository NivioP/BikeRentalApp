using BikeRentalApp.Application.DTOs;

namespace BikeRentalApp.Application.Interfaces {
    public interface IMotoService {
        Task<MotoDto> CreateAsync(MotoCreateDto createDto);
        Task<IEnumerable<MotoDto>> GetAllAsync();
        Task<MotoDto> GetByIdAsync(string id);
        Task UpdateAsync(string id, MotoUpdatePlacaDto updateDto);
        Task DeleteAsync(string id);
    }
}
