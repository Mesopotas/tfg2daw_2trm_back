using Models;
using CoWorking.DTO;

namespace CoWorking.Service
{
    public interface ISalasService
    {
        Task<List<SalasDTO>> GetAllAsync();
        Task<SalasDTO> GetByIdAsync(int id);
      Task<List<SalasDTO>> GetByIdSedeAsync(int id);
        Task AddAsync(SalasDTO sala);
      //  Task UpdateAsync(Salas sala);
        Task DeleteAsync(int id);
    }
}