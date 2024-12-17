using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Domain.Entities;
using BikeRentalApp.Domain.Events;
using BikeRentalApp.Domain.Interfaces;

namespace BikeRentalApp.Application.Services {
    public class MotoService : IMotoService {
        private readonly IMotoRepository _motoRepository;
        private readonly IEventPublisher _eventPublisher;

        public MotoService(IMotoRepository motoRepository, IEventPublisher eventPublisher) {
            _motoRepository = motoRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task CreateAsync(MotoCreateDto createDto) {
            if (await _motoRepository.PlacaExistsAsync(createDto.Placa)) {
                throw new Exception("Essa placa já está registrada.");
            }

            if ((await _motoRepository.GetByIdAsync(createDto.Identificador)) != null) {
                throw new Exception("Esse Identificador já está registrado.");
            }

            var moto = new Moto {
                Identificador = createDto.Identificador,
                Ano = createDto.Ano,
                Modelo = createDto.Modelo,
                Placa = createDto.Placa
            };

            await _motoRepository.AddAsync(moto);

            var motoEvent = new MotoCreatedEvent {
                Identificador = moto.Identificador,
                Ano = moto.Ano,
                Modelo = moto.Modelo,
                Placa = moto.Placa
            };

            await _eventPublisher.PublishAsync("moto.created", motoEvent);
        }

        public async Task<IEnumerable<MotoDto>> GetAllAsync(string? placa) {
            var motos = await _motoRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(placa)) {
                motos = motos.Where(m => m.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
            }

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

        public async Task UpdatePlacaAsync(string id, MotoUpdatePlacaDto updateDto) {
            var moto = await _motoRepository.GetByIdAsync(id);
            if (moto == null) {
                throw new Exception("Moto não encontrada.");
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
