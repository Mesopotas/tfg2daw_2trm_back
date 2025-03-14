using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public interface IOpinionesAsientosRepository
    {
        Task<List<OpinionesAsientos>> GetAllAsync();
        Task<OpinionesAsientos?> GetByIdAsync(int id);
        Task AddAsync(OpinionesAsientos opinionesAsientos);
        
        Task UpdateAsync(OpinionesAsientos opinionesAsientos);
        Task DeleteAsync(int id);

    }
}