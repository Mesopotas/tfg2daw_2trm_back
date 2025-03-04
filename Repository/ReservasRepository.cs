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

        public async Task<List<ReservasDTO>> GetAllAsync()
        {
            var reservas = new List<ReservasDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                 SELECT  
                reserva.IdReserva, reserva.Fecha, reserva.Descripcion, reserva.PrecioTotal,
                usuario.IdUsuario, usuario.Nombre , usuario.Email,
                detallesr.IdDetalleReserva, puesto.IdPuestoTrabajo, puesto.CodigoMesa, 
                puesto.URL_Imagen, detallesr.Descripcion
                FROM Reservas reserva
                INNER JOIN Usuarios usuario ON reserva.IdUsuario = usuario.IdUsuario
                INNER JOIN Lineas linea ON reserva.IdReserva = linea.IdReserva
                INNER JOIN DetallesReservas detallesr ON linea.IdDetalleReserva = detallesr.IdDetalleReserva
                INNER JOIN PuestosTrabajo puesto ON detallesr.IdPuestoTrabajo = puesto.IdPuestoTrabajo;";


                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var reserva = new ReservasDTO
                        {
                            IdReserva = reader.GetInt32(0),
                            Fecha = reader.GetDateTime(1),
                            ReservaDescripcion = reader.GetString(2),
                            PrecioTotal = (double)reader.GetDecimal(3),
                            UsuarioId = reader.GetInt32(4),
                            UsuarioNombre = reader.GetString(5),
                            UsuarioEmail = reader.GetString(6),
                            DetallesReservas = new List<DetalleReservaDTO>()
                        };

                        if (!reader.IsDBNull(7))   // busca el detalle de la reserva si existe
                        {
                            reserva.DetallesReservas.Add(new DetalleReservaDTO
                            {
                                IdDetalleReserva = reader.GetInt32(7),
                                IdPuestoTrabajo = reader.GetInt32(8),
                                CodigoPuesto = reader.GetString(9),
                                ImagenPuesto = reader.GetString(10),
                                Descripcion = reader.GetString(11)
                            });
                        }

                        reservas.Add(reserva);
                    }
                }
            }
            return reservas;
        }

        public async Task<ReservasDTO> GetByIdAsync(int id)
        {
            ReservasDTO reserva = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
            SELECT  
                reserva.IdReserva, reserva.Fecha, reserva.Descripcion, reserva.PrecioTotal,
                usuario.IdUsuario, usuario.Nombre , usuario.Email,
                detallesr.IdDetalleReserva, puesto.IdPuestoTrabajo, puesto.CodigoMesa, 
                puesto.URL_Imagen, detallesr.Descripcion
            FROM Reservas reserva
            INNER JOIN Usuarios usuario ON reserva.IdUsuario = usuario.IdUsuario
            INNER JOIN Lineas linea ON reserva.IdReserva = linea.IdReserva
            INNER JOIN DetallesReservas detallesr ON linea.IdDetalleReserva = detallesr.IdDetalleReserva
            INNER JOIN PuestosTrabajo puesto ON detallesr.IdPuestoTrabajo = puesto.IdPuestoTrabajo
            WHERE reserva.IdReserva = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            reserva = new ReservasDTO
                            {
                                IdReserva = reader.GetInt32(0),
                                Fecha = reader.GetDateTime(1),
                                ReservaDescripcion = reader.GetString(2),
                                PrecioTotal = (double)reader.GetDecimal(3),
                                UsuarioId = reader.GetInt32(4),
                                UsuarioNombre = reader.GetString(5),
                                UsuarioEmail = reader.GetString(6),
                                DetallesReservas = new List<DetalleReservaDTO>()
                            };


                            if (!reader.IsDBNull(7))
                            {
                                reserva.DetallesReservas.Add(new DetalleReservaDTO
                                {
                                    IdDetalleReserva = reader.GetInt32(7),
                                    IdPuestoTrabajo = reader.GetInt32(8),
                                    CodigoPuesto = reader.GetString(9),
                                    ImagenPuesto = reader.GetString(10),
                                    Descripcion = reader.GetString(11)
                                });
                            }
                        }
                    }
                }
            }
            return reserva;
        }

        public async Task AddAsync(ReservasDTO reserva)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // insert reserva, le procederá un insert en detalles reserva, y le seguirá un insert en Linea en el fronend con stores de pinia
                string query = "INSERT INTO Reservas (IdUsuario, IdDetalleReserva, Fecha, Precio) VALUES (@IdUsuario, @IdDetalleReserva, @Fecha, @Precio)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", reserva.UsuarioId);
                    command.Parameters.AddWithValue("@IdDetalleReserva", reserva.DetallesReservasId);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    command.Parameters.AddWithValue("@Precio", reserva.PrecioTotal);


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