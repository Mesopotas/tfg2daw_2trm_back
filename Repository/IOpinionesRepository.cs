using Models;

namespace Cinema.Repositories
{
    public interface IOpinionesRepository
    {
        Task<List<Opiniones>> GetAllAsync();
        Task<Opiniones?> GetByIdAsync(int id);
        Task AddAsync(Opiniones opinion);
        Task UpdateAsync(Opiniones opinion);
        Task DeleteAsync(int id);
    }
}
