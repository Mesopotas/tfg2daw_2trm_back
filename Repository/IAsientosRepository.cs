using Models;

namespace Cinema.Repositories
{
    public interface IAsientosRepository
    {
        Task<List<Asientos>> GetAllAsync();

        Task<Asientos?> GetByIdSalasAsync(int idSala, int id);
        Task<Asientos?> GetByIdAsync(int id);
        

        //Task AddAsync(Asientos asientos);
        //Task UpdateAsync(Asientos asientos);
        //Task DeleteAsync(int id);  
    }
}