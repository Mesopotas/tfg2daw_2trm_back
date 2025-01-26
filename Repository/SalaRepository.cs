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

                string query = "SELECT idSala, nombre, capacidad FROM Salas";
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
                                Capacidad = reader.GetInt32(2)
                            };
                            salas.Add(sala);
                        }
                    }
                }
            }
            return salas;
        }

        public async Task<Salas> GetByIdAsync(int id)
        {
            Salas sala = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idSala, nombre, capacidad FROM Salas WHERE idSala = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sala = new Salas
                            {
                                IdSala = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Capacidad = reader.GetInt32(2)
                            };
                        }
                    }
                }
            }
            return sala;
        }

        public async Task AddAsync(Salas sala)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Salas (nombre, capacidad) VALUES (@Nombre, @Capacidad)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", sala.Nombre);
                    command.Parameters.AddWithValue("@Capacidad", sala.Capacidad);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Salas sala)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Salas SET nombre = @Nombre, email = @Email, capacidad = @Capacidad";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", sala.IdSala);
                    command.Parameters.AddWithValue("@Nombre", sala.Nombre);
                    command.Parameters.AddWithValue("@Capacidad", sala.Capacidad);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Salas WHERE idSala = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {


        }

    }
}