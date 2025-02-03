using Models;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;

namespace Cinema.Service
{
    public interface IGruposAsientosService
    {
        Task<List<GruposAsientos>> GetAllAsync();
        Task<GruposAsientos?> GetByIdAsync(int id);
        Task AddAsync(GruposAsientos gruposAsientos);
        Task UpdateAsync(GruposAsientos gruposAsientos);
        Task DeleteAsync(int id);
        // Task InicializarDatosAsync();      
    }
}
