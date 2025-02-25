using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoWorking.Repositories
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
                string query = "SELECT IdSala, Nombre, URL_Imagen, Capacidad, IdTipoSala, IdSede, Precio, Bloqueado FROM Salas";
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
                                URL_Imagen = reader.GetString(2),
                                Capacidad = reader.GetInt32(3),
                                IdTipoSala = reader.GetInt32(4),
                                IdSede = reader.GetInt32(5),
                                Precio = reader.GetDecimal(6),
                                Bloqueado = reader.GetBoolean(7),
                                Puestos = new List<PuestosTrabajo>(),
                                Zona = new List<ZonasTrabajo>()

                            };

                            // zonas de trabajo para esta sala
                            string queryZonasTrabajo = "SELECT IdZonaTrabajo, NumPuestosTrabajo, Descripcion FROM ZonasTrabajo WHERE IdSala = @idSala";

                            using (var commandZonaTrabajo = new SqlCommand(queryZonasTrabajo, connection))
                            {
                                commandZonaTrabajo.Parameters.AddWithValue("@idSala", sala.IdSala);

                                using (var readerZonaTrabajo = await commandZonaTrabajo.ExecuteReaderAsync())
                                {
                                    while (await readerZonaTrabajo.ReadAsync())
                                    {
                                        var zonaTrabajo = new ZonasTrabajo(
                                            idZonaTrabajo: readerZonaTrabajo.GetInt32(0),
                                            numPuestosTrabajo: readerZonaTrabajo.GetInt32(1),
                                            descripcion: readerZonaTrabajo.GetString(2)

                                        );

                                        sala.Zona.Add(zonaTrabajo);
                                    }
                                }
                            }
                            // puestos de trabajo para esta sala

                            string queryPuestosTrabajo = "SELECT IdPuestoTrabajo, URL_Imagen, Codigo, Estado, Capacidad, TipoPuesto, Bloqueado FROM PuestosTrabajo WHERE IdSala = @idSala";

                            using (var commandPuestoTrabajo = new SqlCommand(queryPuestosTrabajo, connection))
                            {
                                commandPuestoTrabajo.Parameters.AddWithValue("@idSala", sala.IdSala);

                                using (var readerPuestosTrabajos = await commandPuestoTrabajo.ExecuteReaderAsync())
                                {
                                    while (await readerPuestosTrabajos.ReadAsync())
                                    {
                                        var puestoTrabajo = new PuestosTrabajo(
                                            idPuestoTrabajo: readerPuestosTrabajos.GetInt32(0),
                                            urlImagen: readerPuestosTrabajos.GetString(1),
                                            codigo: readerPuestosTrabajos.GetInt32(2),
                                            estado: readerPuestosTrabajos.GetInt32(3),
                                            capacidad: readerPuestosTrabajos.GetInt32(4),
                                            tipoPuesto: readerPuestosTrabajos.GetInt32(5),
                                            bloqueado: readerPuestosTrabajos.GetBoolean(6)
                                        );

                                        sala.Puestos.Add(puestoTrabajo);
                                    }
                                }
                            }


                            salas.Add(sala);
                        }
                    }
                }
            }

            return salas;
        }
        public async Task<Salas> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string querySala = "SELECT IdSala, Nombre, URL_Imagen, Capacidad, IdTipoSala, IdSede, Precio, Bloqueado FROM Salas WHERE IdSala = @Id";
                using (var command = new SqlCommand(querySala, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var sala = new Salas
                            {
                                IdSala = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                URL_Imagen = reader.GetString(2),
                                Capacidad = reader.GetInt32(3),
                                IdTipoSala = reader.GetInt32(4),
                                IdSede = reader.GetInt32(5),
                                Precio = reader.GetDecimal(6),
                                Bloqueado = reader.GetBoolean(7),
                                Puestos = new List<PuestosTrabajo>(),
                                Zona = new List<ZonasTrabajo>()
                            };

                            // Zonas de trabajo para esta sala
                            string queryZonasTrabajo = "SELECT IdZonaTrabajo, NumPuestosTrabajo, Descripcion FROM ZonasTrabajo WHERE IdSala = @idSala";
                            using (var commandZonaTrabajo = new SqlCommand(queryZonasTrabajo, connection))
                            {
                                commandZonaTrabajo.Parameters.AddWithValue("@idSala", sala.IdSala);
                                using (var readerZonaTrabajo = await commandZonaTrabajo.ExecuteReaderAsync())
                                {
                                    while (await readerZonaTrabajo.ReadAsync())
                                    {
                                        var zonaTrabajo = new ZonasTrabajo(
                                            idZonaTrabajo: readerZonaTrabajo.GetInt32(0),
                                            numPuestosTrabajo: readerZonaTrabajo.GetInt32(1),
                                            descripcion: readerZonaTrabajo.GetString(2)
                                        );

                                        sala.Zona.Add(zonaTrabajo);
                                    }
                                }
                            }

                            string queryPuestosTrabajo = "SELECT IdPuestoTrabajo, URL_Imagen, Codigo, Estado, Capacidad, TipoPuesto, Bloqueado FROM PuestosTrabajo WHERE IdSala = @idSala";
                            using (var commandPuestoTrabajo = new SqlCommand(queryPuestosTrabajo, connection))
                            {
                                commandPuestoTrabajo.Parameters.AddWithValue("@idSala", sala.IdSala);
                                using (var readerPuestosTrabajos = await commandPuestoTrabajo.ExecuteReaderAsync())
                                {
                                    while (await readerPuestosTrabajos.ReadAsync())
                                    {
                                        var puestoTrabajo = new PuestosTrabajo(
                                            idPuestoTrabajo: readerPuestosTrabajos.GetInt32(0),
                                            urlImagen: readerPuestosTrabajos.GetString(1),
                                            codigo: readerPuestosTrabajos.GetInt32(2),
                                            estado: readerPuestosTrabajos.GetInt32(3),
                                            capacidad: readerPuestosTrabajos.GetInt32(4),
                                            tipoPuesto: readerPuestosTrabajos.GetInt32(5),
                                            bloqueado: readerPuestosTrabajos.GetBoolean(6)
                                        );

                                        sala.Puestos.Add(puestoTrabajo);
                                    }
                                }
                            }

                            return sala;
                        }
                    }
                }
            }

            return null;
        }


        public async Task AddAsync(Salas sala)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string insertSala = @"
        INSERT INTO Salas (Nombre, URL_Imagen, Capacidad, IdTipoSala, IdSede, Precio, Bloqueado) 
        VALUES (@Nombre, @URL_Imagen, @Capacidad, @IdTipoSala, @IdSede, @Precio, @Bloqueado);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int idSala;

                using (var command = new SqlCommand(insertSala, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", sala.Nombre);
                    command.Parameters.AddWithValue("@URL_Imagen", sala.URL_Imagen);
                    command.Parameters.AddWithValue("@Capacidad", sala.Capacidad);
                    command.Parameters.AddWithValue("@IdTipoSala", sala.IdTipoSala);
                    command.Parameters.AddWithValue("@IdSede", sala.IdSede);
                    command.Parameters.AddWithValue("@Precio", sala.Precio);
                    command.Parameters.AddWithValue("@Bloqueado", sala.Bloqueado);

                    idSala = (int)await command.ExecuteScalarAsync();
                }

                string insertZonaTrabajoParaSalas = @"
        INSERT INTO ZonasTrabajo (NumPuestosTrabajo, Descripcion, IdSala) 
        VALUES (@NumPuestosTrabajo, @Descripcion, @IdSala);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int idZonaTrabajo; // accesible para el proximo insert
                using (var command = new SqlCommand(insertZonaTrabajoParaSalas, connection))
                {
                    command.Parameters.AddWithValue("@NumPuestosTrabajo", sala.Capacidad);
                    command.Parameters.AddWithValue("@Descripcion", "");
                    command.Parameters.AddWithValue("@IdSala", idSala);
                    idZonaTrabajo = (int)await command.ExecuteScalarAsync();

                }

                for (int i = 1; i <= sala.Capacidad; i++)
                {
                    string insertarPuestosTrabajo = @"
            INSERT INTO PuestosTrabajo (URL_Imagen, Codigo, Estado, Capacidad, TipoPuesto, IdZonaTrabajo, IdTipoPuestoTrabajo, IdSala, Bloqueado) 
            VALUES (@URL_Imagen, @Codigo, @Estado, @Capacidad, @TipoPuesto, @IdZonaTrabajo, @IdTipoPuestoTrabajo, @IdSala, @Bloqueado)";

                    using (var command = new SqlCommand(insertarPuestosTrabajo, connection))
                    {
                        command.Parameters.AddWithValue("@URL_Imagen", "");
                        command.Parameters.AddWithValue("@Codigo", 1);
                        command.Parameters.AddWithValue("@Estado", 1); // 1 seria disponible
                        command.Parameters.AddWithValue("@Capacidad", 1); // para sillas, ajustable con la posibilidad de añadir mesas
                        command.Parameters.AddWithValue("@TipoPuesto", 1); // POSIBLE BORRARLO DE LA BBDD
                        command.Parameters.AddWithValue("@IdZonaTrabajo", idZonaTrabajo); // de la zona de trabajo de la consulta de arriba
                        command.Parameters.AddWithValue("@IdTipoPuestoTrabajo", 1); // 1 será una silla, si no existe dará error
                        command.Parameters.AddWithValue("@IdSala", idSala);
                        command.Parameters.AddWithValue("@Bloqueado", 0); // para el admin, por defecto no bloqueado

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task UpdateAsync(Salas sala)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Salas SET Nombre = @Nombre, URL_Imagen = @URL_Imagen, Capacidad = @Capacidad, IdTipoSala = @IdTipoSala, IdSede = @IdSede, Precio = @Precio, Bloqueado = @Bloqueado WHERE IdSala = @IdSala";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", sala.IdSala);
                    command.Parameters.AddWithValue("@Nombre", sala.Nombre);
                    command.Parameters.AddWithValue("@URL_Imagen", sala.URL_Imagen);
                    command.Parameters.AddWithValue("@Capacidad", sala.Capacidad);
                    command.Parameters.AddWithValue("@IdTipoSala", sala.IdTipoSala);
                    command.Parameters.AddWithValue("@IdSede", sala.IdSede);
                    command.Parameters.AddWithValue("@Precio", sala.Precio);
                    command.Parameters.AddWithValue("@Bloqueado", sala.Bloqueado);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // eliminar puestos trabajo
                string deletePuestosTrabajo = "DELETE FROM PuestosTrabajo WHERE IdSala = @IdSala";
                using (var command = new SqlCommand(deletePuestosTrabajo, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", id);
                    await command.ExecuteNonQueryAsync();
                }

                // eliminar zona trabajo
                string deleteZonasTrabajo = "DELETE FROM ZonasTrabajo WHERE IdSala = @IdSala";
                using (var command = new SqlCommand(deleteZonasTrabajo, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", id);
                    await command.ExecuteNonQueryAsync();
                }

                // eliminar sala
                string deleteSala = "DELETE FROM Salas WHERE IdSala = @IdSala";
                using (var command = new SqlCommand(deleteSala, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}