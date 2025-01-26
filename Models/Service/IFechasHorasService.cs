using Models;
using Cinema.Controllers;
using Cinema.Repositories;
using Cinema.Service;

namespace Cinema.Service
{
    public interface IFechasHorasService
    {
        Task<List<FechasHoras>> GetAllAsync();
        Task<FechasHoras?> GetByIdAsync(int id);
        Task AddAsync(FechasHoras fechahora);
        Task UpdateAsync(FechasHoras fechahora);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();
    }
}
