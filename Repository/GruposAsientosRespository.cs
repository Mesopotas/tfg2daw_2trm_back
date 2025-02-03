using Microsoft.Data.SqlClient;
using Models;

namespace Cinema.Repositories
{
    public class GruposAsientosRepository : IGruposAsientosRepository
    {
        private readonly string _connectionString;

        public GruposAsientosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<GruposAsientos>> GetAllAsync()
        {
            var gruposAsientos = new List<GruposAsientos>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IDGRUPO, DESCRIPCION, PRECIO FROM GRUPO_ASIENTOS";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var grupoAsiento = new GruposAsientos
                            {
                                IdGruposSesiones = reader.GetInt32(0),
                                Descripcion = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2)
                            };

                            gruposAsientos.Add(grupoAsiento);
                        }
                    }
                }
            }
            return gruposAsientos;
        }

        
        public async Task<GruposAsientos> GetByIdAsync(int id)
        {
            GruposAsientos grupoAsiento = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IDGRUPO, DESCRIPCION, PRECIO FROM GRUPO_ASIENTOS WHERE IDGRUPO = @IdGruposSesiones";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdGruposSesiones", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            grupoAsiento = new GruposAsientos
                            {
                                IdGruposSesiones = reader.GetInt32(0),
                                Descripcion = reader.GetString(1),
                                Precio = (double)reader.GetDecimal(2)
                            };
                        }
                    }
                }
            }
            return grupoAsiento;
        }

        public async Task AddAsync(GruposAsientos gruposAsientos) 
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO GRUPO_ASIENTOS (DESCRIPCION, PRECIO) VALUES (@Descripcion, @Precio)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", gruposAsientos.Descripcion);   
                    command.Parameters.AddWithValue("@Precio", gruposAsientos.Precio);   

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(GruposAsientos grupoAsientoActualizar)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE GRUPO_ASIENTOS SET DESCRIPCION = @Descripcion, PRECIO = @Precio WHERE IDGRUPO = @IdGruposSesiones";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdGruposSesiones", grupoAsientoActualizar.IdGruposSesiones);
                    command.Parameters.AddWithValue("@Descripcion", grupoAsientoActualizar.Descripcion);
                    command.Parameters.AddWithValue("@Precio", grupoAsientoActualizar.Precio);


                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM GRUPO_ASIENTOS WHERE IDGRUPO = @IdGruposSesiones";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdGruposSesiones", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}