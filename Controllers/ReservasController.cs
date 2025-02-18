using Microsoft.AspNetCore.Mvc;
using CoWorking.Repositories;
using CoWorking.Service;
using Models;

namespace CoWorking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private static List<Reservas> reservas = new List<Reservas>();

        private readonly IReservasService _serviceReservas;

        public ReservasController(IReservasService service)
        {
            _serviceReservas = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservas>>> GetReservas()
        {
            var reservas = await _serviceReservas.GetAllAsync();
            return Ok(reservas);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Reservas>> GetReserva(int id)
        {
            var sala = await _serviceReservas.GetByIdAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            return Ok(sala);
        }


        [HttpPost]
        public async Task<ActionResult<Reservas>> CreateReserva(Reservas reserva)
        {
            await _serviceReservas.AddAsync(reserva);
            return CreatedAtAction(nameof(CreateReserva), new { id = reserva.IdReserva }, reserva);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReserva(int id, Reservas updatedReservas)
        {
            var existingReserva = await _serviceReservas.GetByIdAsync(id);
            if (existingReserva == null)
            {
                return NotFound();
            }
            existingReserva.IdUsuario = updatedReservas.IdUsuario;
            existingReserva.IdLinea = updatedReservas.IdLinea;


            await _serviceReservas.UpdateAsync(existingReserva);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var sala = await _serviceReservas.GetByIdAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            await _serviceReservas.DeleteAsync(id);
            return NoContent();



        }

    }
}