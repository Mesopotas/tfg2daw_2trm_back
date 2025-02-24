using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public interface ITiposSalasRepository
    {
        Task<List<TipoSalas>> GetAllAsync();
        Task<TipoSalas?> GetByIdAsync(int id);
        Task AddAsync(TipoSalas tipoSalas);
        Task UpdateAsync(TipoSalas tipoSalas);
        Task DeleteAsync(int id);

    }
}