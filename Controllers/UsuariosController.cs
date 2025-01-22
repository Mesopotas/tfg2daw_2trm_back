using Microsoft.AspNetCore.Mvc;
using Cinema.Repositories;
using Models;

namespace Cinema.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UsuariosController : ControllerBase
   {
    private static List<Usuarios> usuarios = new List<Usuarios>();

    private readonly IUsuariosRepository _repository;

    public UsuariosController(IUsuariosRepository repository)
        {
            _repository = repository;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Usuarios>>> GetUsuarios()
        {
            var usuarios = await _repository.GetAllAsync();
            return Ok(usuarios);
        }
   }
}