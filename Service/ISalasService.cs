using Models;

namespace Cinema.Service
{
    public interface ISalasService
    {
        Task<List<Salas>> GetAllAsync();
        Task<Salas?> GetByIdAsync(int id);
        Task AddAsync(Salas salas);
        Task UpdateAsync(Salas salas);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();      
    }
}