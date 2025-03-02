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

                string query = "SELECT IdLinea, IdReserva, IdDetalleReserva, Precio FROM Lineas";
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

                await connection.OpenAsync();

                // Consultas SQL para poder conseguir los ids
                string queryIdPuestoTrabajo = "SELECT IdPuestoTrabajo FROM DetallesReservas WHERE IdDetalleReserva = @IdDetalleReserva";
                string queryIdSala = "SELECT IdSala FROM PuestosTrabajo WHERE IdPuestoTrabajo = @IdPuestosTrabajoQuery";
                string queryIdTipoSala = "SELECT IdTipoSala FROM Salas WHERE IdSala = @IdSalaQuery";
                string queryIdTipoPuestoTrabajo = "SELECT IdTipoPuestoTrabajo FROM TiposSalas WHERE IdTipoSala = @IdTipoSalaQuery";
                string queryPrecio = "SELECT Precio FROM TiposPuestosTrabajo WHERE IdTipoPuestoTrabajo = @queryIdTipoPuestoTrabajoQuery";

                // Obtener valores de la base de datos
                int idPuestoTrabajo;
                using (var command = new SqlCommand(queryIdPuestoTrabajo, connection))
                {
                    command.Parameters.AddWithValue("@IdDetalleReserva", linea.IdDetalleReserva);
                    var result = await command.ExecuteScalarAsync();
                    idPuestoTrabajo = result != null ? Convert.ToInt32(result) : 0;
                }

                int idSala;
                using (var command = new SqlCommand(queryIdSala, connection))
                {
                    command.Parameters.AddWithValue("@IdPuestosTrabajoQuery", idPuestoTrabajo);
                    var result = await command.ExecuteScalarAsync();
                    idSala = result != null ? Convert.ToInt32(result) : 0;
                }

                int idTipoSala;
                using (var command = new SqlCommand(queryIdTipoSala, connection))
                {
                    command.Parameters.AddWithValue("@IdSalaQuery", idSala);
                    var result = await command.ExecuteScalarAsync();
                    idTipoSala = result != null ? Convert.ToInt32(result) : 0;
                }

                int idTipoPuestoTrabajo;
                using (var command = new SqlCommand(queryIdTipoPuestoTrabajo, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoSalaQuery", idTipoSala);
                    var result = await command.ExecuteScalarAsync();
                    idTipoPuestoTrabajo = result != null ? Convert.ToInt32(result) : 0;
                }

                decimal precio;
                using (var command = new SqlCommand(queryPrecio, connection))
                {
                    command.Parameters.AddWithValue("@queryIdTipoPuestoTrabajoQuery", idTipoPuestoTrabajo);
                    var result = await command.ExecuteScalarAsync();
                    precio = result != null ? Convert.ToDecimal(result) : 0m;
                }

                // Insertar en la tabla Lineas con los valores obtenidos
                string queryInsert = "INSERT INTO Lineas (IdReserva, IdDetalleReserva, Precio) VALUES (@IdReserva, @IdDetalleReserva, @Precio)";
                using (var command = new SqlCommand(queryInsert, connection))
                {
                    command.Parameters.AddWithValue("@IdReserva", linea.IdReserva);
                    command.Parameters.AddWithValue("@IdDetalleReserva", linea.IdDetalleReserva);
                    command.Parameters.AddWithValue("@Precio", precio);

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