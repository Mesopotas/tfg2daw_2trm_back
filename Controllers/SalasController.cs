using Microsoft.AspNetCore.Mvc;
using Cinema.Repositories;
using Cinema.Service;
using Models;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private static List<Salas> salas = new List<Salas>();

        private readonly ISalasService _serviceSalas;

        public SalasController(ISalasService service)
        {
            _serviceSalas = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Salas>>> GetSalas()
        {
            var salas = await _serviceSalas.GetAllAsync();
            return Ok(salas);
        }
    }
}