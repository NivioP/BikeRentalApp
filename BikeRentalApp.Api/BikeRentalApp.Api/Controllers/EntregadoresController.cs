using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;


namespace BikeRentalApp.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EntregadoresController : ControllerBase {
        private readonly IEntregadorService _entregadorService;
        private readonly S3Service _s3Service;

        public EntregadoresController(IEntregadorService entregadorService, S3Service s3Service) {
            _entregadorService = entregadorService;
            _s3Service = s3Service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EntregadorCreateDto createDto) {
            try {
                await _entregadorService.CreateAsync(createDto);

                return StatusCode(201);
            }
            catch (Exception) {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpPost("{id}/cnh")]
        public async Task<IActionResult> UpdateCnh([FromForm] EntregadorUpdateCNHDto dto) {

            if (!IsValidBase64ImageFormat(dto.Imagem_CNH)) {
                return BadRequest("Formato de imagem inválido. Apenas PNG e BMP são permitidos.");
            }

            try {
                var fileName = $"cnh-{Guid.NewGuid()}.{GetImageExtension(dto.Imagem_CNH)}";
                var storagePath = "nivio-rentalbikeapp/cnh-images/";

                var fileBytes = Convert.FromBase64String(dto.Imagem_CNH);
                using (var stream = new MemoryStream(fileBytes)) {
                    await _s3Service.UploadFileAsync(fileName, stream, storagePath);
                }

                var fileUrl = _s3Service.GetFileUrl(fileName);
                return Ok(new { Message = "CNH atualizada com sucesso.", UrlImagemCNH = fileUrl });
            }
            catch (Exception ex) {
                return StatusCode(500, $"Erro ao atualizar a CNH: {ex.Message}");
            }
        }

        private bool IsValidBase64ImageFormat(string base64String) {
            try {
                var base64PrefixRemoved = base64String.Replace("data:image/png;base64,", "").Replace("data:image/bmp;base64,", "");

                var base64Bytes = Convert.FromBase64String(base64PrefixRemoved);
                var base64StringFormatted = Convert.ToBase64String(base64Bytes);

                return base64PrefixRemoved == base64StringFormatted;
            }
            catch (FormatException) {
                return false;
            }
        }

        private string GetImageExtension(string base64String) {
            var base64Header = base64String.Split(',')[0];
            if (base64Header.Contains("image/png")) {
                return ".png";
            }
            else if (base64Header.Contains("image/bmp")) {
                return ".bmp";
            }
            return string.Empty;
        }

    }
}
