using Models;

namespace Cinema.Repositories
{
    public interface IAsientosService
    {
        Task<List<Asientos>> GetAllAsync();

        Task<Asientos?> GetByIdSalasAsync( int id);
        Task<Asientos?> GetByIdAsync(int idSala, int id);
        

        //Task AddAsync(Asientos asientos);
        Task UpdateAsync(Asientos asientos);
        //Task DeleteAsync(int id);  
    }
}