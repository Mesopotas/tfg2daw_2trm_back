using Microsoft.AspNetCore.Mvc;
using CoWorking.Repositories;
using CoWorking.Service;
using CoWorking.DTO;
using Models;

namespace CoWorking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        private static List<Favoritos> favorito = new List<Favoritos>();

        private readonly IFavoritosService _serviceFavoritos;

        public FavoritosController(IFavoritosService service)
        {
            _serviceFavoritos = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Favoritos>>> GetDisponibilidades()
        {
            var disponibilidades = await _serviceFavoritos.GetAllAsync();
            return Ok(disponibilidades);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Favoritos>>> GetDisponibilidad(int id)
        {
            var favorito = await _serviceFavoritos.GetByIdAsync(id);
            if (favorito == null)
            {
                return NotFound();
            }
            return Ok(favorito);
        }
    [HttpPost]
        public async Task<ActionResult<Favoritos>> CreateRole(Favoritos favorito)
        {
            await _serviceFavoritos.AddAsync(favorito);
            return CreatedAtAction(nameof(CreateRole), new { id = favorito.IdFavorito }, favorito);
        }

    [HttpDelete("{idUsuario}/{idSala}")]
public async Task<IActionResult> DeleteLinea(int idUsuario, int idSala)
{

    await _serviceFavoritos.DeleteAsync(idUsuario, idSala);

    return NoContent();
}
    }
}