using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public interface ISalasRepository
    {
        Task<List<Salas>> GetAllAsync();
        Task<Salas?> GetByIdAsync(int id);
        Task AddAsync(Salas salas);
        Task UpdateAsync(Salas salas);
        Task DeleteAsync(int id);

    }
}