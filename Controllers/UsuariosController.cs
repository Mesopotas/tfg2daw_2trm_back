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


                [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuario(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }


        [HttpPost]
        public async Task<ActionResult<Usuarios>> CreateUsuario(Usuarios usuarios)
        {
            await _repository.AddAsync(usuarios);
            return CreatedAtAction(nameof(CreateUsuario), new { id = usuarios.IdUsuario }, usuarios);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuarios updatedUsuarios)
        {
            var existingUsuario = await _repository.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            existingUsuario.Nombre = updatedUsuarios.Nombre;
            existingUsuario.Email = updatedUsuarios.Email;
            existingUsuario.Password = updatedUsuarios.Password;
            existingUsuario.FechaRegistro = updatedUsuarios.FechaRegistro;

            await _repository.UpdateAsync(existingUsuario);
            return NoContent();
        }

                [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();



        }
   }
}