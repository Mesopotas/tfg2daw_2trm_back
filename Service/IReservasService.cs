using Models;
using CoWorking.DTO;

namespace CoWorking.Service
{
    public interface IReservasService
    {
        Task<List<ReservasDTO>> GetAllAsync();
        Task<ReservasDTO> GetByIdAsync(int id);
        Task AddAsync(ReservasDTO reserva);
      //  Task UpdateAsync(Reservas reserva);
        Task DeleteAsync(int id);
    }
}