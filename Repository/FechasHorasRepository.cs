using Microsoft.Data.SqlClient;
using Models;

namespace Cinema.Repositories
{
    public class FechasHorasRepository : IFechasHorasRepository
    {
        private readonly string _connectionString;

        public FechasHorasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<FechasHoras>> GetAllAsync()
        {
            var fechasHoras = new List<FechasHoras>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idFechaHora, fecha FROM FechasHoras";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var fecha = new FechasHoras
                            {
                                IdFechaHora = reader.GetInt32(0),
                                Fecha = reader.GetDateTime(1)
                            };

                            fechasHoras.Add(fecha);
                        }
                    }
                }
            }
            return fechasHoras;
        }

        public async Task<FechasHoras> GetByIdAsync(int id)
        {
            FechasHoras fecha = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idFechaHora, fecha FROM FechasHoras WHERE idFechaHora = @IdFechaHora";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdFechaHora", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            fecha = new FechasHoras
                            {
                                IdFechaHora = reader.GetInt32(0),
                                Fecha = reader.GetDateTime(1)
                            };
                        }
                    }
                }
            }
            return fecha;
        }

        public async Task AddAsync(FechasHoras FechaAnadir) // agregado el Anadir para evitar confundirse con el atributo de nombre Fecha
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO FechasHoras (fecha) VALUES (@Fecha)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fecha", FechaAnadir.Fecha);                 



                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(FechasHoras fechaActualizar)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE FechasHoras SET fecha = @Fecha WHERE idFechaHora = @IdFechaHora";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdFechasHoras", fechaActualizar.Fecha);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM FechasHoras WHERE idFechaHora = @IdFechaHora";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdFechaHora", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {


        }

    }
}