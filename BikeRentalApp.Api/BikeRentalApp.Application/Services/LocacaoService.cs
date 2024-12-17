using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Domain.Entities;
using BikeRentalApp.Domain.Interfaces;

namespace BikeRentalApp.Application.Services {
    public class LocacaoService : ILocacaoService {

        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IMotoRepository _motoRepository;

        public LocacaoService(ILocacaoRepository locacaoRepository, IEntregadorRepository entregadorRepository, IMotoRepository motoRepository) {
            _locacaoRepository = locacaoRepository;
            _entregadorRepository = entregadorRepository;
            _motoRepository = motoRepository;
        }

        public async Task CreateLocacaoAsync(LocacaoCreateDto dto) {
            var planDaysPossibility = new List<int> { 7, 15, 30, 45, 50 };

            if (!planDaysPossibility.Contains(dto.Plano)) {
                throw new ArgumentException("Plano inválido.");
            }

            var entregador = await _entregadorRepository.GetByIdAsync(dto.Entregador_Id);

            if (entregador == null) {
                throw new Exception("Entregador não encontrado.");
            }

            if (entregador.Tipo_CNH != "A") {
                throw new Exception("Entregador não habilitado para locação.");
            }

            var moto = await _motoRepository.GetByIdAsync(dto.Moto_Id);
            if (moto == null) {
                throw new Exception("Moto não encontrada.");
            }

            decimal valorDiaria = dto.Plano switch {
                7 => 30,
                15 => 28,
                30 => 22,
                45 => 20,
                50 => 18,
                _ => throw new ArgumentException("Plano inválido.")
            };

            var dataInicio = DateTime.UtcNow.AddDays(1).Date;
            var dataPrevisaoTermino = dataInicio.AddDays(dto.Plano - 1);

            var locacao = new Locacao {
                Identificador = Guid.NewGuid().ToString(),
                Valor_Diaria = valorDiaria,
                Entregador_Id = dto.Entregador_Id,
                Moto_Id = dto.Moto_Id,
                Data_Inicio = dataInicio,
                Data_Previsao_Termino = dataPrevisaoTermino,
                Data_Termino = dataPrevisaoTermino
            };

            await _locacaoRepository.CreateAsync(locacao);
        }

        public async Task<LocacaoDto> GetByIdAsync(string id) {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null) {
                throw new Exception("Locação não encontrada.");
            }

            return new LocacaoDto {
                Identificador = locacao.Identificador,
                Valor_Diaria = locacao.Valor_Diaria,
                Entregador_Id = locacao.Entregador_Id,
                Moto_Id = locacao.Moto_Id,
                Data_Inicio = locacao.Data_Inicio,
                Data_Previsao_Termino = locacao.Data_Previsao_Termino,
                Data_Termino = locacao.Data_Termino
            };
        }

        public async Task<decimal> UpdateDevolucaoAsync(string id, LocacaoDevolucaoUpdateDto dto) {
            var locacao = await _locacaoRepository.GetByIdAsync(id);
            if (locacao == null) {
                throw new Exception("Locação não encontrada.");
            }

            locacao.Data_Devolucao = dto.Data_Devolucao;

            if (dto.Data_Devolucao < locacao.Data_Previsao_Termino) {
                var diasNaoUtilizados = (locacao.Data_Previsao_Termino - dto.Data_Devolucao).Days;
                decimal multaPercentual = locacao.Valor_Diaria switch {
                    30 => 0.20m, 
                    28 => 0.40m, 
                    _ => 0m      
                };

                var valorMulta = diasNaoUtilizados * locacao.Valor_Diaria * multaPercentual;
                var valorTotal = ((dto.Data_Devolucao - locacao.Data_Inicio).Days + 1) * locacao.Valor_Diaria + valorMulta;

                await _locacaoRepository.UpdateAsync(locacao);
                return valorTotal;
            }
            else if (dto.Data_Devolucao > locacao.Data_Previsao_Termino) {
                var diasAtraso = (dto.Data_Devolucao - locacao.Data_Previsao_Termino).Days;
                var valorAdicional = diasAtraso * 50;
                var valorTotal = ((locacao.Data_Previsao_Termino - locacao.Data_Inicio).Days + 1) * locacao.Valor_Diaria + valorAdicional;

                await _locacaoRepository.UpdateAsync(locacao);
                return valorTotal;
            }
            else {
                var valorTotal = ((locacao.Data_Previsao_Termino - locacao.Data_Inicio).Days + 1) * locacao.Valor_Diaria;

                await _locacaoRepository.UpdateAsync(locacao);
                return valorTotal;
            }
        }
    }
}
