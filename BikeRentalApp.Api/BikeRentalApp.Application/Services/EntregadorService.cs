using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Domain.Entities;
using BikeRentalApp.Domain.Interfaces;


namespace BikeRentalApp.Application.Services {
    public class EntregadorService : IEntregadorService {
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly IS3Service _s3Service;

        public EntregadorService(IEntregadorRepository entregadorRepository, IS3Service s3Service) {
            _entregadorRepository = entregadorRepository;
            _s3Service = s3Service;
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

        public async Task<string> UpdateCnhAsync(string entregadorId, string imagemBase64) {
            var entregador = await _entregadorRepository.GetByIdAsync(entregadorId);
            if (entregador == null)
                throw new ArgumentException("Entregador não encontrado.");

            if (!IsValidBase64Image(imagemBase64, out string extension)) {
                throw new ArgumentException("Formato de imagem inválido. Apenas PNG e BMP são suportados.");
            }

            var fileName = $"cnh-{entregadorId}-{Guid.NewGuid()}{extension}";
            var fileBytes = Convert.FromBase64String(RemoveBase64Prefix(imagemBase64));

            var storagePath = "nivio-rentalbikeapp/cnh-images/";
            using (var stream = new MemoryStream(fileBytes)) {
                await _s3Service.UploadFileAsync(fileName, stream, storagePath);
            }

            var fileUrl = _s3Service.GetFileUrl(fileName);

            entregador.Imagem_CNH = fileUrl;
            await _entregadorRepository.UpdateAsync(entregador);

            return fileUrl;
        }

        private bool IsValidBase64Image(string base64String, out string extension) {
            extension = string.Empty;
            try {
                if (base64String.StartsWith("data:image/png;base64,")) {
                    extension = ".png";
                    base64String = RemoveBase64Prefix(base64String);
                }
                else if (base64String.StartsWith("data:image/bmp;base64,")) {
                    extension = ".bmp";
                    base64String = RemoveBase64Prefix(base64String);
                }
                else {
                    return false;
                }

                Convert.FromBase64String(base64String);
                return true;
            }
            catch {
                return false;
            }
        }

        private string RemoveBase64Prefix(string base64String) {
            return base64String.Contains(",") ? base64String.Split(',')[1] : base64String;
        }
    }
}
