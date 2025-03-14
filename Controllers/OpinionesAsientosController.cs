using Microsoft.AspNetCore.Mvc;
using CoWorking.Repositories;
using CoWorking.Service;
using CoWorking.DTO;
using Models;

namespace CoWorking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionesAsientosController : ControllerBase
    {
        private static List<OpinionesAsientos> opinionesAsiento = new List<OpinionesAsientos>();

        private readonly IOpinionesAsientosService _serviceOpinionesAsientos;

        public OpinionesAsientosController(IOpinionesAsientosService service)
        {
            _serviceOpinionesAsientos = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OpinionesAsientos>>> GetOpinionesAsientos()
        {
            var opinionesAsientos = await _serviceOpinionesAsientos.GetAllAsync();
            return Ok(opinionesAsientos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OpinionesAsientos>> GetOpinionesAsiento(int id)
        {
            var opinionesAsiento = await _serviceOpinionesAsientos.GetByIdAsync(id);
            if (opinionesAsiento == null)
            {
                return NotFound();
            }
            return Ok(opinionesAsiento);
        }


        [HttpPost]
        public async Task<ActionResult<OpinionesAsientos>> CreateOpinionesAsiento(OpinionesAsientos opinionesAsientos)
        {
            await _serviceOpinionesAsientos.AddAsync(opinionesAsientos);
            return CreatedAtAction(nameof(CreateOpinionesAsiento), new { id = opinionesAsientos.IdOpinionAsiento }, opinionesAsientos);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOpinionesAsiento(int id, OpinionesAsientos updatedOpinionesAsientos)
        {
            var existingOpinionesAsiento = await _serviceOpinionesAsientos.GetByIdAsync(id);
            if (existingOpinionesAsiento == null)
            {
                return NotFound();
            }
            existingOpinionesAsiento.Opinion = updatedOpinionesAsientos.Opinion;
            existingOpinionesAsiento.FechaOpinion = updatedOpinionesAsientos.FechaOpinion;
            existingOpinionesAsiento.IdPuestoTrabajo = updatedOpinionesAsientos.IdPuestoTrabajo;
            existingOpinionesAsiento.IdUsuario = updatedOpinionesAsientos.IdUsuario;


            await _serviceOpinionesAsientos.UpdateAsync(existingOpinionesAsiento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpinionesAsiento(int id)
        {
            var opinionesAsiento = await _serviceOpinionesAsientos.GetByIdAsync(id);
            if (opinionesAsiento == null)
            {
                return NotFound();
            }
            await _serviceOpinionesAsientos.DeleteAsync(id);
            return NoContent();
        }

    }
}