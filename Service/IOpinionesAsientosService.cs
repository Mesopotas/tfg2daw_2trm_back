using Models;
using CoWorking.DTO;
using System.Security.Claims;

namespace CoWorking.Service
{
    public interface IOpinionesAsientosService
    {
        Task<List<OpinionesAsientos>> GetAllAsync();
        Task<OpinionesAsientos?> GetByIdAsync(int id);
        Task AddAsync(OpinionesAsientos usuario);
        Task UpdateAsync(OpinionesAsientos opinionesAsiento);
        Task DeleteAsync(int id);
    }
}