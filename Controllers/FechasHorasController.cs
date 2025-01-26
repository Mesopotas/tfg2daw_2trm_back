using Microsoft.AspNetCore.Mvc;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FechasHorasController : ControllerBase
    {
        private static List<FechasHoras> fechas = new List<FechasHoras>();

        private readonly IFechasHorasService _serviceFechas;

        public FechasHorasController(IFechasHorasService service)
        {
            _serviceFechas = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<FechasHoras>>> GetFechas()
        {
            var fechas = await _serviceFechas.GetAllAsync();
            return Ok(fechas);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FechasHoras>> GetFecha(int id)
        {
            var fecha = await _serviceFechas.GetByIdAsync(id);
            if (fecha == null)
            {
                return NotFound();
            }
            return Ok(fecha);
        }


        [HttpPost]
        public async Task<ActionResult<FechasHoras>> CreateFecha(FechasHoras fechas)
        {
            await _serviceFechas.AddAsync(fechas);
            return CreatedAtAction(nameof(CreateFecha), new { id = fechas.IdFechaHora }, fechas);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFecha(int id, FechasHoras updatedFecha)
        {
            var existingFecha = await _serviceFechas.GetByIdAsync(id);
            if (existingFecha == null)
            {
                return NotFound();
            }

            existingFecha.Fecha = updatedFecha.Fecha;

            await _serviceFechas.UpdateAsync(existingFecha);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFecha(int id)
        {
            var fecha = await _serviceFechas.GetByIdAsync(id);
            if (fecha == null)
            {
                return NotFound();
            }
            await _serviceFechas.DeleteAsync(id);
            return NoContent();



        }
    }
}