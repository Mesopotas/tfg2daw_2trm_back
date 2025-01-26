using Microsoft.AspNetCore.Mvc;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PeliculaController : ControllerBase
   {
    private static List<Peliculas> peliculas = new List<Peliculas>();

    private readonly IPeliculaService _servicePelicula;

    public PeliculaController(IPeliculaService service)
        {
            _servicePelicula = service;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Peliculas>>> GetPeliculas()
        {
            var peliculas = await _servicePelicula.GetAllAsync();
            return Ok(peliculas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Peliculas>> GetPelicula(int id)
        {
            var pelicula = await _servicePelicula.GetByIdAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return Ok(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult<Peliculas>> CreatePelicula(Peliculas pelicula)
        {
            await _servicePelicula.AddAsync(pelicula);
            return CreatedAtAction(nameof(GetPelicula), new { id = pelicula.IdPelicula }, pelicula);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePelicula(int id, Peliculas updatedPelicula)
        {
            var existingPelicula = await _servicePelicula.GetByIdAsync(id);
            if (existingPelicula == null)
            {
                return NotFound();
            }

            existingPelicula.Titulo = updatedPelicula.Titulo;
            existingPelicula.Sinopsis = updatedPelicula.Sinopsis;
            existingPelicula.Duracion = updatedPelicula.Duracion;
            existingPelicula.Categoria = updatedPelicula.Categoria;
            existingPelicula.Director = updatedPelicula.Director;
            existingPelicula.Anio = updatedPelicula.Anio;
            existingPelicula.ImagenURL = updatedPelicula.ImagenURL;
            existingPelicula.Puntuacion = updatedPelicula.Puntuacion;


            await _servicePelicula.UpdateAsync(existingPelicula);
            return NoContent();
        }

  
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeletePelicula(int id)
       {
           var pelicula = await _servicePelicula.GetByIdAsync(id);
           if (pelicula == null)
           {
               return NotFound();
           }
           await _servicePelicula.DeleteAsync(id);
           return NoContent();
       }

        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            await _servicePelicula.InicializarDatosAsync();
            return Ok("Datos inicializados correctamente.");
        }

   }
}