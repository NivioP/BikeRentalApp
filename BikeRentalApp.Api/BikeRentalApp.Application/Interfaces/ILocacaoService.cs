using BikeRentalApp.Application.DTOs;

namespace BikeRentalApp.Application.Interfaces {
    public interface ILocacaoService {
        Task CreateLocacaoAsync(LocacaoCreateDto dto);
        Task<LocacaoDto> GetByIdAsync(string id);
        Task<decimal> UpdateDevolucaoAsync(string id, LocacaoDevolucaoUpdateDto dataDevolucao);
    }
}
