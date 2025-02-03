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
    }
}