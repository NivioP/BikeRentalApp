using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Domain.Entities;
using BikeRentalApp.Domain.Interfaces;

namespace BikeRentalApp.Application.Services {
    public class MotoService : IMotoService {
        private readonly IMotoRepository _motoRepository;

        public MotoService(IMotoRepository motoRepository) {
            _motoRepository = motoRepository;
        }

        public async Task<MotoDto> CreateAsync(CreateMotoDto createDto) {
            if (await _motoRepository.PlacaExistsAsync(createDto.Placa)) {
                throw new Exception("Placa já registrada.");
            }

            var moto = new Moto {
                Identificador = createDto.Identificador,
                Ano = createDto.Ano,
                Modelo = createDto.Modelo,
                Placa = createDto.Placa
            };

            await _motoRepository.AddAsync(moto);

            return new MotoDto {
                Identificador = moto.Identificador,
                Ano = moto.Ano,
                Modelo = moto.Modelo,
                Placa = moto.Placa
            };
        }

        public async Task<IEnumerable<MotoDto>> GetAllAsync() {
            var motos = await _motoRepository.GetAllAsync();
            return motos.Select(m => new MotoDto {
                Identificador = m.Identificador,
                Ano = m.Ano,
                Modelo = m.Modelo,
                Placa = m.Placa
            });
        }

        public async Task<MotoDto> GetByIdAsync(string id) {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) {
                throw new Exception("Moto não encontrada.");
            }

            return new MotoDto {
                Identificador = moto.Identificador,
                Ano = moto.Ano,
                Modelo = moto.Modelo,
                Placa = moto.Placa
            };
        }

        public async Task UpdateAsync(string id, UpdateMotoPlacaDto updateDto) {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) {
                throw new Exception("Moto not found.");
            }

            moto.Placa = updateDto.Placa;

            await _motoRepository.UpdateAsync(moto);
        }

        public async Task DeleteAsync(string id) {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) {
                throw new Exception("Moto não encontrada.");
            }

            await _motoRepository.DeleteAsync(id);
        }
    }
}
