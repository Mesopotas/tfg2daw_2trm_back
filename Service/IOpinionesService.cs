using Models;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;

namespace Cinema.Service
{
    public interface IOpinionesService
    {
        Task<List<Opiniones>> GetAllAsync();
        Task<Opiniones?> GetByIdAsync(int id);
        Task AddAsync(Opiniones opiniones);
        Task UpdateAsync(Opiniones opiniones);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
