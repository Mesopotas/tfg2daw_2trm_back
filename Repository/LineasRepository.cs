using Microsoft.Data.SqlClient;
using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public class LineasRepository : ILineasRepository
    {
        private readonly string _connectionString;

        public LineasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Lineas>> GetAllAsync()
        {
            var lineas = new List<Lineas>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdLinea, IdReserva, IdDetallerReserva, Precio FROM Lineas";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var linea = new Lineas
                            {
                                IdLinea = reader.GetInt32(0),
                                IdReserva = reader.GetInt32(1),
                                IdDetalleReserva = reader.GetInt32(2),
                                Precio = (double)reader.GetDecimal(3)
                            };

                            lineas.Add(linea);
                        }
                    }
                }
            }
            return lineas;
        }

        public async Task<Lineas> GetByIdAsync(int id)
        {
            Lineas linea = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdLinea, IdReserva, IdDetalleReserva, Precio FROM Lineas WHERE IdLinea = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            linea = new Lineas
                            {
                                IdLinea = reader.GetInt32(0),
                                IdReserva = reader.GetInt32(1),
                                IdDetalleReserva = reader.GetInt32(2),
                                Precio = (double)reader.GetDecimal(3)                                
                            };

                        }
                    }
                }
            }
            return linea;
        }

        public async Task AddAsync(Lineas linea)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Lineas (IdReserva, IdDetallerReserva, Precio) VALUES (@IdReserva, @IdDetalleReserva, @Precio)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReserva", linea.IdReserva);
                    command.Parameters.AddWithValue("@IdDetalleReserva", linea.IdDetalleReserva);
                    command.Parameters.AddWithValue("@Precio", linea.Precio);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Lineas WHERE IdLinea = @IdLinea";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdLinea", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}