using Microsoft.AspNetCore.Mvc;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposAsientosController : ControllerBase
    {
        private static List<GruposAsientos> gruposAsientos = new List<GruposAsientos>();

        private readonly IGruposAsientosService _serviceGruposAsientos;

        public GruposAsientosController(IGruposAsientosService service)
        {
            _serviceGruposAsientos = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GruposAsientos>>> GetGruposAsientos()
        {
            var gruposAsientos = await _serviceGruposAsientos.GetAllAsync();
            return Ok(gruposAsientos);
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<GruposAsientos>> GetGrupoAsiento(int id)
        {
            var grupoAsiento = await _serviceGruposAsientos.GetByIdAsync(id);
            if (grupoAsiento == null)
            {
                return NotFound();
            }
            return Ok(grupoAsiento);
        }


        [HttpPost]
        public async Task<ActionResult<GruposAsientos>> CreateFecha(GruposAsientos gruposAsientos)
        {
            await _serviceGruposAsientos.AddAsync(gruposAsientos);
            return CreatedAtAction(nameof(CreateFecha), new { id = gruposAsientos.IdGruposSesiones }, gruposAsientos);
        }


       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupoAsiento(int id, GruposAsientos gruposAsientos)
        {
            var existingGrupoAsiento = await _serviceGruposAsientos.GetByIdAsync(id);
            if (existingGrupoAsiento == null)
            {
                return NotFound();
            }
            existingGrupoAsiento.Descripcion = gruposAsientos.Descripcion;
            existingGrupoAsiento.Precio = gruposAsientos.Precio;

            await _serviceGruposAsientos.UpdateAsync(existingGrupoAsiento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGruposAsientos(int id)
        {
            var grupoAsiento = await _serviceGruposAsientos.GetByIdAsync(id);
            if (grupoAsiento == null)
            {
                return NotFound();
            }
            await _serviceGruposAsientos.DeleteAsync(id);
            return NoContent();



        }


    }
}