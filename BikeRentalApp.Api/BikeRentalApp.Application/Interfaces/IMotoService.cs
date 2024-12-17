using BikeRentalApp.Application.DTOs;

namespace BikeRentalApp.Application.Interfaces {
    public interface IMotoService {
        Task CreateAsync(MotoCreateDto createDto);
        Task<IEnumerable<MotoDto>> GetAllAsync(string? placa);
        Task<MotoDto> GetByIdAsync(string id);
        Task UpdatePlacaAsync(string id, MotoUpdatePlacaDto updateDto);
        Task DeleteAsync(string id);
    }
}
