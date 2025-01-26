using Models;

namespace Cinema.Repositories
{
    public interface ISalasService
    {
        Task<List<Salas>> GetAllAsync();
        Task<Salas?> GetByIdAsync(int id);
        Task AddAsync(Salas sala);
        Task UpdateAsync(Salas sala);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();      
    }
}