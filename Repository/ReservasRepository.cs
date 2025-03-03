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

                string query = "SELECT IdReserva, IdUsuario, Fecha, Descripcion, PrecioTotal FROM Reservas";
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
                                Fecha = reader.GetDateTime(2),
                                Descripcion = reader.GetString(3),
                                PrecioTotal = (double)reader.GetDecimal(4)
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

                string query = "SELECT IdReserva, IdUsuario, Fecha, Descripcion, PrecioTotal FROM Reservas WHERE IdReserva = @Id";
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
                                Fecha = reader.GetDateTime(2),
                                Descripcion = reader.GetString(3),
                                PrecioTotal = (double)reader.GetDecimal(4)

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

                // Consultas SQL para obtener los datos necesarios
                string queryIdUsuario = "SELECT IdUsuario FROM Usuarios WHERE IdUsuario = @IdUsuario";
                string queryPrecioTotal = "SELECT SUM(Precio) FROM Lineas WHERE IdReserva = @IdReserva";

                int idUsuario;
                using (var command = new SqlCommand(queryIdUsuario, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", reserva.IdUsuario);
                    var result = await command.ExecuteScalarAsync();
                    idUsuario = result != null ? Convert.ToInt32(result) : 0; // si no es nulo, lo parsea a int, si es nulo, le asigna valor 0, esto hará q de error identificable en lugar de un valor nulo no esperado
                }

                // Calcular el PrecioTotal (sumando los precios de las lineas de la reserva)
                decimal precioTotal = 0;
                using (var command = new SqlCommand(queryPrecioTotal, connection))
                {
                    command.Parameters.AddWithValue("@IdReserva", reserva.IdReserva);
                    var result = await command.ExecuteScalarAsync();
                    precioTotal = result != null ? Convert.ToDecimal(result) : 0m; // si no es nulo, lo parsea a decimal, si es nulo, le asigna valor 0 (0m es el valor en decimal de 0)
                }

                // insert final en reservas con toda la info
                string queryInsert = "INSERT INTO Reservas (IdUsuario, Fecha, Descripcion, PrecioTotal) VALUES (@IdUsuario, @Fecha, @Descripcion, @PrecioTotal)";
                using (var command = new SqlCommand(queryInsert, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario); // idUsuario previo
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    command.Parameters.AddWithValue("@Descripcion", reserva.Descripcion);
                    command.Parameters.AddWithValue("@PrecioTotal", precioTotal);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Reservas reserva)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Reservas SET Descripcion = @Descripcion WHERE IdReserva = @IdReserva";

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