using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public interface IReservasRepository
    {
        Task<List<Reservas>> GetAllAsync();
        Task<Reservas?> GetByIdAsync(int id);
        Task AddAsync(Reservas reservas);
        Task UpdateAsync(Reservas reservas);
        Task DeleteAsync(int id);

    }
}