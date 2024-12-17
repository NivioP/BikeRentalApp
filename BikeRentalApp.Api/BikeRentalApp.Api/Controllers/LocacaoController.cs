using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalApp.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase {

        private readonly ILocacaoService _locacaoService;

        public LocacaoController(ILocacaoService locacaoService) {
            _locacaoService = locacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocacao(LocacaoCreateDto dto) {
            try {
                await _locacaoService.CreateLocacaoAsync(dto);
                return StatusCode(201);
            }
            catch {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocacao([FromRoute] string id) {
            try {
                var locacao = await _locacaoService.GetByIdAsync(id);
                if (locacao == null)
                    return NotFound();

                return Ok(locacao);
            }
            catch {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> UpdateDevolucaoAsync([FromRoute] string id, LocacaoDevolucaoUpdateDto dto) {
            try {
                var totalValue = await _locacaoService.UpdateDevolucaoAsync(id, dto);
                return Ok(new { mensagem = $"Data de devolução informada com sucesso. Valor total da locação R${totalValue}" });
            }
            catch {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }
    }
}
