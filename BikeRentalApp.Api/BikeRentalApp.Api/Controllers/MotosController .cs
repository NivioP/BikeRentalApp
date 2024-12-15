using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalApp.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MotosController : ControllerBase {
        private readonly IMotoService _motoService;

        public MotosController(IMotoService motoService) {
            _motoService = motoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var motos = await _motoService.GetAllAsync();
            return Ok(motos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) {
            try {
                var moto = await _motoService.GetByIdAsync(id);
                if (moto == null) {
                    return NotFound(new { mensagem = "Moto não encontrada" });
                }
                return Ok(moto);
            }
            catch(Exception) {
                return BadRequest(new { mensagem = "Request mal formada" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMotoDto createDto) {
            try {
                var moto = await _motoService.CreateAsync(createDto);
                return StatusCode(201);
            }
            catch(Exception) {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateMotoPlacaDto updateDto) {
            try {
                await _motoService.UpdateAsync(id, updateDto);
                return Ok(new { mensagem = "Placa modificada com sucesso" });
            }
            catch(Exception) {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            try {
                await _motoService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception) {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }
    }
}
