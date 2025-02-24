using Microsoft.Data.SqlClient;
using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public class TipoSalasRepository : ITipoSalasRepository
    {
        private readonly string _connectionString;

        public TipoSalasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<TipoSalas>> GetAllAsync()
        {
            var tipoSalas = new List<TipoSalas>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdTipoSala, Descripcion FROM TipoSalas";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var tipoSala = new TipoSalas
                            {
                                IdTipoSala = reader.GetInt32(0),
                                Descripcion = reader.GetString(1),
                            };

                            tipoSalas.Add(tipoSala);
                        }
                    }
                }
            }
            return tipoSalas;
        }

        public async Task<TipoSalas> GetByIdAsync(int id)
        {
            TipoSalas tipoSala = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdTipoSala, Descripcion FROM TipoSalas WHERE idTipoSala = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            tipoSala = new TipoSalas
                            {
                                IdTipoSala = reader.GetInt32(0),
                                Descripcion = reader.GetString(1),
                            };

                        }
                    }
                }
            }
            return tipoSala;
        }

        public async Task AddAsync(TipoSalas tipoSala)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO TipoSalas (Descripcion) VALUES (@Descripcion)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", tipoSala.Descripcion);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(TipoSalas tipoSala)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // La columna FechaRegistro no está incluida ya que no debe ser modificada
                string query = "UPDATE TipoSalas SET Descripcion = @Descripcion WHERE idTipoSala = @IdTipoSala";
                // si el idRol asignado no existe dará error (Microsoft.Data.SqlClient.SqlException (0x80131904): The INSERT statement conflicted with the FOREIGN KEY constraint "FK__TipoSalas__IdRol__276EDEB3". The conflict occurred in database "CoworkingDB", table "dbo.Roles", column 'IdRol'.)

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoSala", tipoSala.IdTipoSala);
                    command.Parameters.AddWithValue("@Descripcion", tipoSala.Descripcion);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM TipoSalas WHERE idTipoSala = @IdTipoSala";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdTipoSala", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}