using Models;

namespace Cinema.Repositories
{
    public interface IPeliculaRepository
    {
        Task<List<Peliculas>> GetAllAsync();
        Task<Peliculas?> GetByIdAsync(int id);
        Task AddAsync(Peliculas pelicula);
        Task UpdateAsync(Peliculas pelicula);
        Task DeleteAsync(int id);
       // Task InicializarDatosAsync();      
    }
}