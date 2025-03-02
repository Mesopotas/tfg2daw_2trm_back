using Microsoft.Data.SqlClient;
using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public class ReservasRepository : IReservasRepository
    {
        private readonly string _connectionString;

        public ReservasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Reservas>> GetAllAsync()
        {
            var reservas = new List<Reservas>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdReserva, IdUsuario, Fecha, Descripcion FROM Reservas";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var reserva = new Reservas
                            {
                                IdReserva = reader.GetInt32(0),
                                IdUsuario = reader.GetInt32(1),
                                Fecha = reader.GetDateTime(3),
                                Descripcion = reader.GetString(4)
                            };

                            reservas.Add(reserva);
                        }
                    }
                }
            }
            return reservas;
        }

        public async Task<Reservas> GetByIdAsync(int id)
        {
            Reservas reserva = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdReserva, IdUsuario, Fecha, Descripcion FROM Reservas WHERE idRol = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            reserva = new Reservas
                            {
                                IdReserva = reader.GetInt32(0),
                                IdUsuario = reader.GetInt32(1),
                                Fecha = reader.GetDateTime(3),
                                Descripcion = reader.GetString(4)
                            };

                        }
                    }
                }
            }
            return reserva;
        }

        public async Task AddAsync(Reservas reserva)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Reservas (IdUsuario, Fecha, Descripcion) VALUES (@IdUsuario, @Fecha, @Descripcion)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", reserva.IdUsuario);
                    command.Parameters.AddWithValue("@Fecha", reserva.Descripcion);
                    command.Parameters.AddWithValue("@Descripcion", reserva.Descripcion);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Reservas reserva)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Reservas SET Descripcion = @Descripcion WHERE idRol = @IdRol";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", reserva.Descripcion);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Reservas WHERE idRol = @IdRol";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdRol", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}