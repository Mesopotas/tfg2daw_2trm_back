using Models;

namespace CoWorking.Repositories
{
    public interface ISalasService
    {
        Task<List<Salas>> GetAllAsync();
        Task<Salas?> GetByIdAsync(int id);
        Task AddAsync(Salas sal);
        Task UpdateAsync(Salas sala);
        Task DeleteAsync(int id);
    }
}
