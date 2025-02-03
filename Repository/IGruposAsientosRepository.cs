using Models;

namespace Cinema.Repositories
{
    public interface IGruposAsientosRepository
    {
        Task<List<GruposAsientos>> GetAllAsync();
        // Task<GruposAsientos?> GetByIdAsync(int id);
        // Task AddAsync(GruposAsientos gruposAsientos);
        // Task UpdateAsync(GruposAsientos gruposAsientos);
        // Task DeleteAsync(int id);
        // Task InicializarDatosAsync();      
    }
}