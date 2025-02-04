using Microsoft.Data.SqlClient;
using Models;

namespace Cinema.Repositories
{
    public class SalasRepository : ISalasRepository
    {
        private readonly string _connectionString;

        public SalasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Salas>> GetAllAsync()
        {
            var salas = new List<Salas>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IDSALA, NOMBRE, CAPACIDAD FROM SALA";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var sala = new Salas
                            {
                                IdSala = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Capacidad = reader.GetInt32(2),
                            };

                            salas.Add(sala);
                        }
                    }
                }
            }
            return salas;
        }
    }
}