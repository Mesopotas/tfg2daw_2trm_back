using Microsoft.AspNetCore.Mvc;
using CoWorking.Repositories;
using CoWorking.Service;
using CoWorking.DTO;
using Models;

namespace CoWorking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private static List<Salas> sala = new List<Salas>();

        private readonly ISalasService _serviceSalas;

        public SalasController(ISalasService service)
        {
            _serviceSalas = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Salas>>> GetSalas()
        {
            var salas = await _serviceSalas.GetAllAsync();
            return Ok(salas);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Salas>> GetSala(int id)
        {
            var sala = await _serviceSalas.GetByIdAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            return Ok(sala);
        }

        [HttpGet("/idsede/{id}")]
        public async Task<ActionResult<Salas>> GetSalaSede(int id)
        {
            var sala = await _serviceSalas.GetByIdSedeAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            return Ok(sala);
        }
        [HttpPost]
        public async Task<ActionResult<Salas>> CreateSala(SalasDTO salas)
        {
            await _serviceSalas.AddAsync(salas);
            return CreatedAtAction(nameof(CreateSala), new { id = salas.IdSala }, salas);
        }

/*
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSala(int id, Salas updatedSalas)
        {
            var existingSala = await _serviceSalas.GetByIdAsync(id);
            if (existingSala == null)
            {
                return NotFound();
            }
            existingSala.Nombre = updatedSalas.Nombre;
            existingSala.Capacidad = updatedSalas.Capacidad;
            existingSala.IdTipoSala = updatedSalas.IdTipoSala;
            existingSala.IdSede = updatedSalas.IdSede;


            await _serviceSalas.UpdateAsync(existingSala);
            return NoContent();
        }
*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _serviceSalas.GetByIdAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            await _serviceSalas.DeleteAsync(id);
            return NoContent();
        }

    }
}