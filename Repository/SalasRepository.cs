using Microsoft.Data.SqlClient;
using Models;
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

        public async Task<List<SalasDTO>> GetAllAsync()
        {
            var salasDto = new List<SalasDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT sala.IdSala, sala.Nombre, sala.URL_Imagen, sala.Capacidad, sala.IdSede, sala.Precio, sala.Bloqueado, ts.NumeroMesas, ts.CapacidadAsientos, ts.EsPrivada FROM Salas sala INNER JOIN TiposSalas ts ON sala.IdTipoSala = ts.IdTipoSala";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {

                            var salaDto = new SalasDTO
                            {
                                IdSala = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                URL_Imagen = reader.GetString(2),
                                Capacidad = reader.GetInt32(3),
                                IdSede = reader.GetInt32(4),
                                Precio = (double)reader.GetDecimal(5),
                                Bloqueado = reader.GetBoolean(6),
                                NumeroMesas = reader.GetInt32(7),
                                CapacidadAsientos = reader.GetInt32(8),
                                EsPrivada = reader.GetBoolean(9)
                            };

                            // Zonas de trabajo para esta sala
                            string queryZonasTrabajo = "SELECT IdZonaTrabajo, Descripcion FROM ZonasTrabajo WHERE IdSala = @idSala";
                            using (var commandZonaTrabajo = new SqlCommand(queryZonasTrabajo, connection))
                            {
                                commandZonaTrabajo.Parameters.AddWithValue("@idSala", salaDto.IdSala);
                                using (var readerZonaTrabajo = await commandZonaTrabajo.ExecuteReaderAsync())
                                {
                                    while (await readerZonaTrabajo.ReadAsync())
                                    {
                                        var zonaTrabajo = new ZonasTrabajoDTO
                                        {
                                            IdZonaTrabajo = readerZonaTrabajo.GetInt32(0),
                                            Descripcion = readerZonaTrabajo.GetString(1)
                                        };

                                        salaDto.Zona.Add(zonaTrabajo);
                                    }
                                }
                            }

                            // Puestos de trabajo para esta sala
                            string queryPuestosTrabajo = "SELECT IdPuestoTrabajo, URL_Imagen, CodigoMesa, Disponible, Bloqueado FROM PuestosTrabajo WHERE IdSala = @idSala";
                            using (var commandPuestoTrabajo = new SqlCommand(queryPuestosTrabajo, connection))
                            {
                                commandPuestoTrabajo.Parameters.AddWithValue("@idSala", salaDto.IdSala);
                                using (var readerPuestosTrabajos = await commandPuestoTrabajo.ExecuteReaderAsync())
                                {
                                    while (await readerPuestosTrabajos.ReadAsync())
                                    {
                                        var puestoTrabajo = new PuestosTrabajoDTO
                                        {
                                            IdPuestoTrabajo = readerPuestosTrabajos.GetInt32(0),
                                            URL_Imagen = readerPuestosTrabajos.GetString(1),
                                            CodigoMesa = readerPuestosTrabajos.GetInt32(2),
                                            Disponible = readerPuestosTrabajos.GetBoolean(3),
                                            Bloqueado = readerPuestosTrabajos.GetBoolean(4)
                                        };

                                        salaDto.Puestos.Add(puestoTrabajo);
                                    }
                                }
                            }

                            salasDto.Add(salaDto);
                        }
                    }
                }
            }

            return salasDto;
        }

        public async Task<SalasDTO> GetByIdAsync(int id)
        {
            SalasDTO salaDto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
            SELECT sala.IdSala, sala.Nombre, sala.URL_Imagen, sala.Capacidad, sala.IdSede, sala.Precio, sala.Bloqueado, 
                   ts.NumeroMesas, ts.CapacidadAsientos, ts.EsPrivada 
            FROM Salas sala 
            INNER JOIN TiposSalas ts ON sala.IdTipoSala = ts.IdTipoSala 
            WHERE sala.IdSala = @IdSala";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            salaDto = new SalasDTO
                            {
                                IdSala = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                URL_Imagen = reader.GetString(2),
                                Capacidad = reader.GetInt32(3),
                                IdSede = reader.GetInt32(4),
                                Precio = (double)reader.GetDecimal(5),
                                Bloqueado = reader.GetBoolean(6),
                                NumeroMesas = reader.GetInt32(7),
                                CapacidadAsientos = reader.GetInt32(8),
                                EsPrivada = reader.GetBoolean(9),
                                Zona = new List<ZonasTrabajoDTO>(),
                                Puestos = new List<PuestosTrabajoDTO>()
                            };
                        }
                    }
                }

                if (salaDto != null)
                {
                    // Zonas de trabajo para esta sala
                    string queryZonasTrabajo = "SELECT IdZonaTrabajo, Descripcion FROM ZonasTrabajo WHERE IdSala = @idSala";
                    using (var commandZonaTrabajo = new SqlCommand(queryZonasTrabajo, connection))
                    {
                        commandZonaTrabajo.Parameters.AddWithValue("@idSala", salaDto.IdSala);
                        using (var readerZonaTrabajo = await commandZonaTrabajo.ExecuteReaderAsync())
                        {
                            while (await readerZonaTrabajo.ReadAsync())
                            {
                                var zonaTrabajo = new ZonasTrabajoDTO
                                {
                                    IdZonaTrabajo = readerZonaTrabajo.GetInt32(0),
                                    Descripcion = readerZonaTrabajo.GetString(1)
                                };
                                salaDto.Zona.Add(zonaTrabajo);
                            }
                        }
                    }

                    // Puestos de trabajo para esta sala
                    string queryPuestosTrabajo = "SELECT IdPuestoTrabajo, URL_Imagen, CodigoMesa, Disponible, Bloqueado FROM PuestosTrabajo WHERE IdSala = @idSala";
                    using (var commandPuestoTrabajo = new SqlCommand(queryPuestosTrabajo, connection))
                    {
                        commandPuestoTrabajo.Parameters.AddWithValue("@idSala", salaDto.IdSala);
                        using (var readerPuestosTrabajos = await commandPuestoTrabajo.ExecuteReaderAsync())
                        {
                            while (await readerPuestosTrabajos.ReadAsync())
                            {
                                var puestoTrabajo = new PuestosTrabajoDTO
                                {
                                    IdPuestoTrabajo = readerPuestosTrabajos.GetInt32(0),
                                    URL_Imagen = readerPuestosTrabajos.GetString(1),
                                    CodigoMesa = readerPuestosTrabajos.GetInt32(2),
                                    Disponible = readerPuestosTrabajos.GetBoolean(3),
                                    Bloqueado = readerPuestosTrabajos.GetBoolean(4)
                                };
                                salaDto.Puestos.Add(puestoTrabajo);
                            }
                        }
                    }
                }
            }

            return salaDto; // Returns null if no sala is found with the given id
        }
        public async Task AddAsync(SalasDTO salaDto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();


                string insertTipoSala = @"
        INSERT INTO TiposSalas (Nombre, NumeroMesas, CapacidadAsientos, EsPrivada, Descripcion, IdTipoPuestoTrabajo) 
        VALUES (@Nombre, @NumeroMesas, @CapacidadAsientos, @EsPrivada, @Descripcion, @IdTipoPuestoTrabajo);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int idTipoSala;

                using (var command = new SqlCommand(insertTipoSala, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", $"Tipo para {salaDto.Nombre}");
                    command.Parameters.AddWithValue("@NumeroMesas", salaDto.EsPrivada ? 1 : salaDto.NumeroMesas); // si esPrivada = true, solo habrá una mesa siempre, si es = false se usa el valor dado a NumeroMesas 
                    command.Parameters.AddWithValue("@CapacidadAsientos", salaDto.CapacidadAsientos);
                    command.Parameters.AddWithValue("@EsPrivada", salaDto.EsPrivada);
                    command.Parameters.AddWithValue("@Descripcion", "");
                    command.Parameters.AddWithValue("@IdTipoPuestoTrabajo", 1); // 1 será el Id de silla común

                    idTipoSala = (int)await command.ExecuteScalarAsync();
                }

                // capacidad total de salas serán las 2 capacidades de tipo de sala
            int capacidadTotal = (salaDto.EsPrivada ? 1 : salaDto.NumeroMesas) * salaDto.CapacidadAsientos; // como arriba, checkeo de si es privada o no para determinar el valor para el que luego se recorrerá en el for de los asientos

                //  Creamos la sala referenciando al tipo creado
                string insertSala = @"
        INSERT INTO Salas (Nombre, URL_Imagen, Capacidad, IdTipoSala, IdSede, Bloqueado) 
        VALUES (@Nombre, @URL_Imagen, @Capacidad, @IdTipoSala, @IdSede, @Bloqueado);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int idSala;

                using (var command = new SqlCommand(insertSala, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", salaDto.Nombre);
                    command.Parameters.AddWithValue("@URL_Imagen", salaDto.URL_Imagen);
                    command.Parameters.AddWithValue("@Capacidad", capacidadTotal);
                    command.Parameters.AddWithValue("@IdTipoSala", idTipoSala);
                    command.Parameters.AddWithValue("@IdSede", salaDto.IdSede);
                    command.Parameters.AddWithValue("@Bloqueado", false);

                    idSala = (int)await command.ExecuteScalarAsync();
                }

                //   insert a zona de trabajo
                string insertZonaTrabajo = @"
        INSERT INTO ZonasTrabajo (Descripcion, IdSala) 
        VALUES (@Descripcion, @IdSala);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int idZonaTrabajo;
                using (var command = new SqlCommand(insertZonaTrabajo, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", "");
                    command.Parameters.AddWithValue("@IdSala", idSala);

                    idZonaTrabajo = (int)await command.ExecuteScalarAsync();
                }

                //  se crean las sillas en base a la capacidad total
                for (int i = 0; i < capacidadTotal; i++)
                {
                    // cada silla tendrá asociada una mesa a la que se asocie
                    int codigoMesa = (i / salaDto.CapacidadAsientos) + 1; // al ser una operacion de enteros, no hay decimales, por tanto 3/4 = 0, 5/4 = 1 y asi con todos, pudiendo asi autonincrementar los valores

                    string insertPuestoTrabajo = @"
            INSERT INTO PuestosTrabajo (URL_Imagen, CodigoMesa, IdZonaTrabajo, IdSala, Bloqueado) 
            VALUES (@URL_Imagen, @CodigoMesa, @IdZonaTrabajo, @IdSala, @Bloqueado)";

                    using (var command = new SqlCommand(insertPuestoTrabajo, connection))
                    {
                        command.Parameters.AddWithValue("@URL_Imagen", ""); // sin imagen por defecto de momento (añadir para el fetch)
                        command.Parameters.AddWithValue("@CodigoMesa", codigoMesa);
                        command.Parameters.AddWithValue("@IdZonaTrabajo", idZonaTrabajo);
                        command.Parameters.AddWithValue("@IdSala", idSala);
                        command.Parameters.AddWithValue("@Bloqueado", false);

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

                // get para el id del tipo de sala asociado a la sala
                int? idTipoSala = null; // ? es un nullable, permite inicializar la variable entera con nulo
                string getTipoSalaQuery = "SELECT IdTipoSala FROM Salas WHERE IdSala = @IdSala";
                using (var command = new SqlCommand(getTipoSalaQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", id);
                    var respuestaIdTipoSala = await command.ExecuteScalarAsync();
                    if (respuestaIdTipoSala != null) // si encuentra un valor
                    {
                        idTipoSala = (int)respuestaIdTipoSala; // lo asigna a la variable
                    }
                }

                // borrar los asientos
                string deletePuestosTrabajo = "DELETE FROM PuestosTrabajo WHERE IdSala = @IdSala";
                using (var command = new SqlCommand(deletePuestosTrabajo, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", id);
                    await command.ExecuteNonQueryAsync();
                }

                // borrar la zona
                string deleteZonasTrabajo = "DELETE FROM ZonasTrabajo WHERE IdSala = @IdSala";
                using (var command = new SqlCommand(deleteZonasTrabajo, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", id);
                    await command.ExecuteNonQueryAsync();
                }

                // borrar la sala (en este orden para evitar conflictos de clave foranea)
                string deleteSala = "DELETE FROM Salas WHERE IdSala = @IdSala";
                using (var command = new SqlCommand(deleteSala, connection))
                {
                    command.Parameters.AddWithValue("@IdSala", id);
                    await command.ExecuteNonQueryAsync();
                }

                // vborrar el tipo de sala, pero solo si no se usa en otra sala comprobandolo para evitar posibles errores, ya que al principio las posibles salas serán predefinidas
                if (idTipoSala.HasValue)
                {
                    string comprobarUsoTipoSalas = "SELECT COUNT(*) FROM Salas WHERE IdTipoSala = @IdTipoSala";
                    using (var command = new SqlCommand(comprobarUsoTipoSalas, connection))
                    {
                        command.Parameters.AddWithValue("@IdTipoSala", idTipoSala.Value);
                        int conteoTipoSalas = (int)await command.ExecuteScalarAsync();
                        if (conteoTipoSalas == 0) // si no se encuentran resultados, significa que no se esta usando por otros datos y por tanto se puede eliminar sin problemas
                        {
                            string deleteTipoSala = "DELETE FROM TiposSalas WHERE IdTipoSala = @IdTipoSala";
                            using (var ejecutarBorradoTipoSala = new SqlCommand(deleteTipoSala, connection))
                            {
                                ejecutarBorradoTipoSala.Parameters.AddWithValue("@IdTipoSala", idTipoSala.Value);
                                await ejecutarBorradoTipoSala.ExecuteNonQueryAsync();
                            }
                        }
                    }
                }
            }
        }
    }
}