using Models;

namespace Cinema.Repositories
{
    public interface IAsientosRepository
    {
        Task<List<Asientos>> GetAllAsync();
        Task<Asientos?> GetByIdAsync(int idSala, int id);
        Task UpdateAsync(Asientos asientos);
        

        //Task AddAsync(Asientos asientos);
        //Task<Asientos?> GetByIdSalasAsync( int id);
        //Task DeleteAsync(int id);  
    }
}