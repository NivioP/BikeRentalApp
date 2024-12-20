﻿using BikeRentalApp.Application.DTOs;
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
        public async Task<IActionResult> GetAll([FromQuery] string? placa) {
            var motos = await _motoService.GetAllAsync(placa);
            return Ok(motos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id) {
            try {
                var moto = await _motoService.GetByIdAsync(id);
               
                return Ok(moto);
            }
            catch(Exception ex) {
                if (ex.Message.Equals("Moto não encontrada")) {
                    return NotFound(new { mensagem = ex.Message });
                }
                return BadRequest(new { mensagem = "Request mal formada" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MotoCreateDto createDto) {
            try {
                await _motoService.CreateAsync(createDto);
                return StatusCode(201);
            }
            catch(Exception) {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpPut("{id}/placa")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] MotoUpdatePlacaDto updateDto) {
            try {
                await _motoService.UpdatePlacaAsync(id, updateDto);
                return Ok(new { mensagem = "Placa modificada com sucesso" });
            }
            catch(Exception) {
                return BadRequest(new { mensagem = "Dados inválidos" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id) {
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
