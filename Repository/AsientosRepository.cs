using Microsoft.Data.SqlClient;
using Models;

namespace Cinema.Repositories
{
    public class AsientosRepository : IAsientosRepository
    {
        private readonly string _connectionString;

        public AsientosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Asientos>> GetAllAsync()
        {
            var asientos = new List<Asientos>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idAsiento, idSala, numAsientos, precio, estado FROM Asientos";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var asiento = new Asientos
                            {
                                IdAsiento = reader.GetInt32(0),
                                IdSala = reader.GetInt32(1),
                                NumAsiento = reader.GetInt32(2),
                                Estado = reader.GetBoolean(3),
                                Precio = reader.GetDouble(4)
                            };

                            asientos.Add(asiento);
                        }
                    }
                }
            }
            return asientos;
        }

        public async Task<Asientos> GetByIdSalasAsync( int id)
        {
            Asientos asiento = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idAsiento, idSala, numAsientos, precio, estado FROM Asientos WHERE idAsiento = @IdAsiento";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAsiento", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            asiento = new Asientos
                            {
                                IdAsiento = reader.GetInt32(0),
                                IdSala = reader.GetInt32(1),
                                NumAsiento = reader.GetInt32(2),
                                Estado = reader.GetBoolean(3),
                                Precio = reader.GetDouble(4)
                            };
                        }
                    }
                }
            }
            return asiento;
        }


        public async Task<Asientos> GetByIdAsync(int idSala, int id)
        {
            Asientos asiento = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idAsiento, idSala, numAsientos, precio, estado FROM Asientos WHERE idSala = @IdSala AND idAsiento = @IdAsiento ";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", idSala);
                    command.Parameters.AddWithValue("@IdAsiento", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            asiento = new Asientos
                            {
                                IdAsiento = reader.GetInt32(0),
                                IdSala = reader.GetInt32(1),
                                NumAsiento = reader.GetInt32(2),
                                Estado = reader.GetBoolean(3),
                                Precio = reader.GetDouble(4)
                            };
                        }
                    }
                }
            }
            return asiento;
        }

        
        public async Task UpdateAsync(Asientos asientos)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Asientos SET Estado = @Estado WHERE idSala = @IdSala AND numAsiento = @NumAsiento;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Estado", Asientos.Estado);
                    command.Parameters.AddWithValue("@IdSala", Asientos.IdSala);
                    command.Parameters.AddWithValue("@NumAsiento", Asientos.NumAsiento);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        



    }
}
