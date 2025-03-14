using Microsoft.Data.SqlClient;
using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public class OpinionesAsientosRepository : IOpinionesAsientosRepository
    {
        private readonly string _connectionString;

        public OpinionesAsientosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<OpinionesAsientos>> GetAllAsync()
        {
            var opinionesAsientos = new List<OpinionesAsientos>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdOpinionAsiento, Opinion, FechaOpinion, IdPuestoTrabajo, IdUsuario FROM OpinionesAsientos";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var opinionesAsiento = new OpinionesAsientos
                            {
                                IdOpinionAsiento = reader.GetInt32(0),
                                Opinion = reader.GetString(1),
                                FechaOpinion = reader.GetDateTime(2),
                                IdPuestoTrabajo = reader.GetInt32(3),
                                IdUsuario = reader.GetInt32(4)
                            };

                            opinionesAsientos.Add(opinionesAsiento);
                        }
                    }
                }
            }
            return opinionesAsientos;
        }

        public async Task<OpinionesAsientos> GetByIdAsync(int id)
        {
            OpinionesAsientos opinionesAsiento = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT  IdOpinionAsiento, Opinion, FechaOpinion, IdPuestoTrabajo, IdUsuario FROM OpinionesAsientos WHERE IdOpinionAsiento = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            opinionesAsiento = new OpinionesAsientos
                            {
                                IdOpinionAsiento = reader.GetInt32(0),
                                Opinion = reader.GetString(1),
                                FechaOpinion = reader.GetDateTime(2),
                                IdPuestoTrabajo = reader.GetInt32(3),
                                IdUsuario = reader.GetInt32(4)
                            };
                        }
                    }
                }
            }
            return opinionesAsiento;
        }

        public async Task AddAsync(OpinionesAsientos opinioneasiento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO OpinionesAsientos (Opinion, FechaOpinion, IdPuestoTrabajo, IdUsuario) VALUES (@Opinion, @FechaOpinion, @IdPuestoTrabajo, @IdUsuario)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Opinion", opinioneasiento.Opinion);
                    command.Parameters.AddWithValue("@FechaOpinion", opinioneasiento.FechaOpinion);
                    command.Parameters.AddWithValue("@IdPuestoTrabajo", opinioneasiento.IdPuestoTrabajo);
                    command.Parameters.AddWithValue("@IdUsuario", opinioneasiento.IdUsuario);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(OpinionesAsientos opinioneasiento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE OpinionesAsientos SET Opinion = @Opinion, FechaOpinion = @FechaOpinion, IdPuestoTrabajo = @IdPuestoTrabajo, IdUsuario = @IdUsuario WHERE IdOpinionAsiento = @IdOpinionAsiento";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdOpinionAsiento", opinioneasiento.IdOpinionAsiento);
                    command.Parameters.AddWithValue("@Opinion", opinioneasiento.Opinion);
                    command.Parameters.AddWithValue("@FechaOpinion", opinioneasiento.FechaOpinion);
                    command.Parameters.AddWithValue("@IdPuestoTrabajo", opinioneasiento.IdPuestoTrabajo);
                    command.Parameters.AddWithValue("@IdUsuario", opinioneasiento.IdUsuario);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM OpinionesAsientos WHERE IdOpinionAsiento = @IdOpinionAsiento";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdOpinionAsiento", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
