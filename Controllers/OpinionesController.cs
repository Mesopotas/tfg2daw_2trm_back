using Microsoft.AspNetCore.Mvc;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class OpinionesController : ControllerBase
   {
    private static List<Opiniones> opiniones = new List<Opiniones>();

    private readonly IOpinionesService _serviceOpiniones;

    public OpinionesController(IOpinionesService service)
        {
            _serviceOpiniones = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Opiniones>>> GetOpiniones()
        {
            var opiniones = await _serviceOpiniones.GetAllAsync();
            return Ok(opiniones);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Opiniones>> GetOpinion(int id)
        {
            var opinion = await _serviceOpiniones.GetByIdAsync(id);
            if (opinion == null)
            {
                return NotFound();
            }
            return Ok(opinion);
        }

        [HttpPost]
        public async Task<ActionResult<Opiniones>> CreateOpinion(Opiniones opinion)
        {
            await _serviceOpiniones.AddAsync(opinion);
            return CreatedAtAction(nameof(GetOpinion), new { id = opinion.Id }, opinion);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOpinion(int id, Opiniones updatedOpinion)
        {
            var existingOpinion = await _serviceOpiniones.GetByIdAsync(id);
            if (existingOpinion == null)
            {
                return NotFound();
            }
            /*No incluido en primera instancia la propiedad idUsuario ni idPelicula ni fechaComentario ya que estas no deberian cambiar una vez se ha hecho la reseña*/
            existingOpinion.Comentario = updatedOpinion.Comentario;

            await _serviceOpiniones.UpdateAsync(existingOpinion);
            return NoContent();
        }

  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteOpinion(int id)
       {
           var opinion = await _serviceOpiniones.GetByIdAsync(id);
           if (opinion == null)
           {
               return NotFound();
           }
           await _serviceOpiniones.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _serviceOpiniones.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}