using Models;

namespace Cinema.Repositories
{
    public interface IFechasHorasRepository
    {
        Task<List<FechasHoras>> GetAllAsync();
        Task<FechasHoras?> GetByIdAsync(int id);
        Task AddAsync(FechasHoras fechas);
        Task UpdateAsync(FechasHoras fechas);
        Task DeleteAsync(int id);
       // Task InicializarDatosAsync();      
    }
}