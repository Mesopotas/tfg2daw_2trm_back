using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public interface IUsuariosRepository
    {
        Task<List<Usuarios>> GetAllAsync();
        Task<Usuarios?> GetByIdAsync(int id);
        Task AddAsync(Usuarios usuario);
        Task UpdateAsync(Usuarios usuario);
        Task DeleteAsync(int id);
        Task<List<UsuarioClienteDTO>> GetClientesByEmailAsync(string email); // para obtener solo los datos que el cliente le interesan, optimizando a su vez la api al manejar menos informacion que no es realmente util en un contexto determinado

    }
}