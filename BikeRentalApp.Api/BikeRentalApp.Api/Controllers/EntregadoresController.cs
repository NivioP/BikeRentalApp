using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BikeRentalApp.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EntregadoresController : ControllerBase {
        private readonly IEntregadorService _entregadorService;

        public EntregadoresController(IEntregadorService entregadorService) {
            _entregadorService = entregadorService;
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
        public async Task<IActionResult> UpdateCnh([FromRoute] string id, [FromBody] EntregadorUpdateCNHDto dto) {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Imagem_CNH)) {
                return BadRequest("A imagem da CNH é obrigatória.");
            }

            try {
                var urlImagemCNH = await _entregadorService.UpdateCnhAsync(id, dto.Imagem_CNH);
                return Ok(new { Mensagem = "CNH atualizada com sucesso.", UrlImagemCNH = urlImagemCNH });
            }
            catch (ArgumentException ex) {
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (Exception ex) {
                return StatusCode(500, new { Mensagem = "Erro ao atualizar a CNH.", Detalhe = ex.Message });
            }
        }

    }
}
