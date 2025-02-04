using Models;

namespace Cinema.Repositories
{
    public interface ISalasRepository
    {
        Task<List<Salas>> GetAllAsync();
        Task<Salas?> GetByIdAsync(int id);
        Task AddAsync(Salas sal);
        Task UpdateAsync(Salas sala);
        Task DeleteAsync(int id);
    }
}
