using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Domain.Entities;
using BikeRentalApp.Domain.Interfaces;

namespace BikeRentalApp.Application.Services {
    public class EntregadorService : IEntregadorService {
        private readonly IEntregadorRepository _entregadorRepository;

        public EntregadorService(IEntregadorRepository entregadorRepository) {
            _entregadorRepository = entregadorRepository;
        }

        public async Task CreateAsync(EntregadorCreateDto createDto) {
            if (await _entregadorRepository.NumeroCNHExistsAsync(createDto.Numero_CNH)) {
                throw new Exception("This NumeroCNH is already registered");
            }

            if ((await _entregadorRepository.GetByIdAsync(createDto.Identificador)) != null) {
                throw new Exception("This Identificador is already registered");
            }

            var entregador = new Entregador {
                Identificador = createDto.Identificador,
                Nome = createDto.Nome,
                CNPJ = createDto.CNPJ,
                Data_Nascimento = createDto.Data_Nascimento,
                Numero_CNH = createDto.Numero_CNH,
                Tipo_CNH = createDto.Tipo_CNH,
                Imagem_CNH = createDto.Imagem_CNH,

            };

            await _entregadorRepository.AddAsync(entregador);
        }
    }
}
