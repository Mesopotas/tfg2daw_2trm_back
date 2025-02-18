using Models;

namespace CoWorking.Repositories
{
    public interface IReservasRepository
    {
        Task<List<Reservas>> GetAllAsync();
        Task<Reservas?> GetByIdAsync(int id);
        Task AddAsync(Reservas reserva);
        Task UpdateAsync(Reservas reserva);
        Task DeleteAsync(int id);
    }
}