using Models;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;

namespace Cinema.Service
{
    public interface IPeliculaService
    {
        Task<List<Peliculas>> GetAllAsync();
        Task<Peliculas?> GetByIdAsync(int id);
        Task AddAsync(Peliculas pelicula);
        Task UpdateAsync(Peliculas pelicula);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
