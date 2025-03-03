using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public interface ISalasRepository
    {
        Task<List<SalasDTO>> GetAllAsync();
        Task<SalasDTO> GetByIdAsync(int id);
        Task<List<SalasDTO>> GetByIdSedeAsync(int id);
        Task AddAsync(SalasDTO salasDTO);
      //  Task UpdateAsync(Salas salas);
        Task DeleteAsync(int id);

    }
}