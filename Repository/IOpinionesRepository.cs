using Models;

namespace Cinema.Repositories
{
    public interface IOpinionesRepository
    {
        Task<List<Opiniones>> GetAllAsync();
        Task<Opiniones?> GetByIdAsync(int id);
        Task AddAsync(Opiniones opiniones);
        Task UpdateAsync(Opiniones opiniones);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();      
    }
}