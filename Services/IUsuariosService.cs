using Models;

namespace Cinema.Service
{
    public interface IUsuariosService
    {
        Task<List<Usuarios>> GetAllAsync();
        Task<Usuarios?> GetByIdAsync(int id);
        Task AddAsync(Usuarios usuario);
        Task UpdateAsync(Usuarios usuario);
        Task DeleteAsync(int id);
        Task InicializarDatosAsync();      
    }
}