using Models;
using CoWorking.DTO;

namespace CoWorking.Service
{
    public interface ISalasService
    {
        Task<List<Salas>> GetAllAsync();
        Task<Salas?> GetByIdAsync(int id);
        Task AddAsync(Salas sala);
        Task UpdateAsync(Salas sala);
        Task DeleteAsync(int id);
    }
}