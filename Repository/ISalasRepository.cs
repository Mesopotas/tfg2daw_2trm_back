using Models;

namespace Cinema.Repositories
{
    public interface ISalasRepository
    {
        Task<List<Salas>> GetAllAsync();
        Task<Salas?> GetByIdAsync(int id);
        Task AddAsync(Salas sala);
        Task UpdateAsync(Salas sala);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();      
    }
}