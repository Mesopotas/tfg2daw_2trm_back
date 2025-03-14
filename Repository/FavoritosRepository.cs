using Microsoft.Data.SqlClient;
using Models;
using CoWorking.DTO;

namespace CoWorking.Repositories
{
    public class FavoritosRepository : IFavoritosRepository
    {
        private readonly string _connectionString;

        public FavoritosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<FavoritosDTO>> GetAllAsync()
        {
            var favoritos = new List<FavoritosDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
//     public Favoritos(int idFavorito, int idUsuario, int idSala, string nombreSala, string imagenSala, int capacidad, string tipoSala)

                string query = @"SELECT favorito.IdFavorito, favorito.IdUsuario, favorito.IdSala, sala.Nombre AS NombreSala,  sala.URL_Imagen AS ImagenSala,  sala.Capacidad,
                 tiposala.Nombre AS TipoSala  FROM Favoritos favorito JOIN Salas sala ON favorito.IdSala = sala.IdSala JOIN TiposSalas tiposala ON sala.IdTipoSala = tiposala.IdTipoSala";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var favorito = new FavoritosDTO
                            {
                                IdFavorito = reader.GetInt32(0),
                                IdUsuario = reader.GetInt32(1),
                                IdSala = reader.GetInt32(2),
                                NombreSala = reader.GetString(3),
                                ImagenSala = reader.GetString(4),
                                Capacidad = reader.GetInt32(5),
                                TipoSala = reader.GetString(6),

                            };

                            favoritos.Add(favorito);
                        }
                    }
                }
            }
            return favoritos;
        }

public async Task<List<FavoritosDTO>> GetByIdAsync(int id)
{
    var favoritos = new List<FavoritosDTO>();

    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        string query = @"
            SELECT favorito.IdFavorito, favorito.IdUsuario, favorito.IdSala, 
                   sala.Nombre AS NombreSala, sala.URL_Imagen AS ImagenSala, 
                   sala.Capacidad, tiposala.Nombre AS TipoSala  
            FROM Favoritos favorito 
            JOIN Salas sala ON favorito.IdSala = sala.IdSala 
            JOIN TiposSalas tiposala ON sala.IdTipoSala = tiposala.IdTipoSala 
            WHERE favorito.IdUsuario = @Id";

        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", id);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var favorito = new FavoritosDTO
                    {
                        IdFavorito = reader.GetInt32(0),
                        IdUsuario = reader.GetInt32(1),
                        IdSala = reader.GetInt32(2),
                        NombreSala = reader.GetString(3),
                        ImagenSala = reader.GetString(4),
                        Capacidad = reader.GetInt32(5),
                        TipoSala = reader.GetString(6),
                    };

                    favoritos.Add(favorito); 
                }
            }
        }
    }
    return favoritos;
}


        public async Task AddAsync(Favoritos favorito)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Favoritos (IdUsuario, IdSala) VALUES (@IdUsuario, @IdSala)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", favorito.IdUsuario);
                    command.Parameters.AddWithValue("@IdSala", favorito.IdSala);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

  public async Task DeleteAsync(int idUsuario, int idSala)
{
    using (var connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        // La consulta ahora filtra por ambos, IdUsuario y IdSala
        string query = "DELETE FROM Favoritos WHERE IdUsuario = @IdUsuario AND IdSala = @IdSala";

        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@IdUsuario", idUsuario);
            command.Parameters.AddWithValue("@IdSala", idSala);

            await command.ExecuteNonQueryAsync();
        }
    }
}

            }
        }
