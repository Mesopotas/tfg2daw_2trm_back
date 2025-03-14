using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public interface IFavoritosRepository
    {
        Task<List<FavoritosDTO>> GetAllAsync();
        Task<List<FavoritosDTO>> GetByIdAsync(int id);
        Task AddAsync(Favoritos favorito);
        Task DeleteAsync(int idUsuario, int idSala);

    }
}