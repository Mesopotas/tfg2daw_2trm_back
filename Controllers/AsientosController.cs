
using Microsoft.AspNetCore.Mvc;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsientosController : ControllerBase
    {
        private static List<Asientos> asientos = new List<Asientos>();

        private readonly IAsientosService _serviceAsientos;

        public AsientosController(IAsientosService service)
        {
            _serviceAsientos = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Asientos>>> GetAsientos()
        {
            var asientos = await _serviceAsientos.GetAllAsync();
            return Ok(asientos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Asientos>> GetAsiento(int idSala, int id)
        {
            var asiento = await _serviceAsientos.GetByIdAsync(idSala, id);
            if (asiento == null)
            {
                return NotFound();
            }
            return Ok(asiento);
        }

/*
        [HttpGet("Sala/{id}")]
        public async Task<ActionResult<Asientos>> GetAsientoByIdSala(int id)
        {
            var asiento = await _serviceAsientos.GetByIdSalasAsync( id);
            if (asiento == null)
            {
                return NotFound();
            }
            return Ok(asiento);
        }
        */

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsiento(int idSala, int id, Asientos updateAsiento)
        {
            var existingAsiento = await _serviceAsientos.GetByIdAsync(idSala, id);
            if (existingAsiento == null)
            {
                return NotFound();
            }

            existingAsiento.Estado = updateAsiento.Estado;

            await _serviceAsientos.UpdateAsync(existingAsiento);
            return NoContent();
        }


    }
}

