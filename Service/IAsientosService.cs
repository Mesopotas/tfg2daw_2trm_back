using Models;

namespace Cinema.Repositories
{
    public interface IAsientosService
    {
        Task<List<Asientos>> GetAllAsync();
        Task UpdateAsync(Asientos asientos);
        Task<Asientos?> GetByIdAsync(int idSala, int id);

        
        //Task AddAsync(Asientos asientos);
        //Task<Asientos?> GetByIdSalasAsync( int id);
        //Task DeleteAsync(int id);  
    }
}