using Microsoft.Data.SqlClient;
using Models;

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

                string query = "SELECT IdReserva, IdUsuario, IdLinea FROM Reservas";
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
                                IdLinea = reader.GetInt32(2)
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

                string query = " SELECT IdReserva, IdUsuario, IdLinea FROM Reservas WHERE IdReserva = @Id";
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
                                IdLinea = reader.GetInt32(2)
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

        string query = "INSERT INTO Reservas (IdUsuario, IdLinea) VALUES (@IdUsuario, @IdLinea)";
        
        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@IdUsuario", reserva.IdUsuario);
            command.Parameters.AddWithValue("@IdLinea", reserva.IdLinea);
            await command.ExecuteNonQueryAsync();
        }
    }
}

public async Task UpdateAsync(Reservas reserva)
{
    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        string query = "UPDATE Reservas SET IdUsuario = @IdUsuario, IdLinea = @IdLinea WHERE IdReserva = @IdReserva";

        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@IdUsuario", reserva.IdUsuario);
            command.Parameters.AddWithValue("@IdLinea", reserva.IdLinea);
            await command.ExecuteNonQueryAsync();
        }
    }
}

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Reservas WHERE IdReserva = @IdReserva";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdReserva", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

  
    }
}