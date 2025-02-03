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
    }
}