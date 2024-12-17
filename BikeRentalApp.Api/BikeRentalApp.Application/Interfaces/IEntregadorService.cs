using BikeRentalApp.Application.DTOs;

namespace BikeRentalApp.Application.Interfaces {
    public interface IEntregadorService {
        Task CreateAsync(EntregadorCreateDto createDto);
        Task<string> UpdateCnhAsync(string entregadorId, string imagemBase64);
    }
}
