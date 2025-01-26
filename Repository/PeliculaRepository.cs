using Microsoft.Data.SqlClient;
using Models;

namespace Cinema.Repositories
{
    public class PeliculasRepository : IPeliculaRepository
    {
        private readonly string _connectionString;

        public PeliculasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Peliculas>> GetAllAsync()
        {
            var peliculas = new List<Peliculas>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idPelicula, titulo, sinopsis , duracion , categoria, director, anio, imagenURL, puntuacion FROM Peliculas";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pelicula = new Peliculas
                            {
                                IdPelicula = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Sinopsis = reader.GetString(2),
                                Duracion = (double)reader.GetDecimal(3),
                                Categoria = reader.GetString(4),
                                Director = reader.GetString(5),
                                Anio = reader.GetDateTime(6),
                                ImagenURL = reader.GetString(7),
                                Puntuacion = reader.GetInt32(8)
                            };

                            peliculas.Add(pelicula);
                        }
                    }
                }
            }
            return peliculas;
        }

        public async Task<Peliculas> GetByIdAsync(int id)
        {
            Peliculas peliculas = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idPelicula, titulo, sinopsis , duracion , categoria, director, anio, imagenURL, puntuacion FROM Peliculas WHERE idPelicula = @IdPelicula";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPelicula", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            peliculas = new Peliculas
                            {
                                IdPelicula = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Sinopsis = reader.GetString(2),
                                Duracion = (double)reader.GetDecimal(3),
                                Categoria = reader.GetString(4),
                                Director = reader.GetString(5),
                                Anio = reader.GetDateTime(6),
                                ImagenURL = reader.GetString(7),
                                Puntuacion = reader.GetInt32(8)
                            };
                        }
                    }
                }
            }
            return peliculas;
        }

        public async Task AddAsync(Peliculas Pelicula)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Peliculas (titulo, sinopsis, duracion, categoria, director, anio, imagenURL, puntuacion) VALUES (@Titutlo, @Sinopsis, @Duracion, @Categoria, @Director, @Anio, @ImagenURL, @Puntuacion)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titutlo", Pelicula.Titulo);
                    command.Parameters.AddWithValue("@Sinopsis", Pelicula.Sinopsis);
                    command.Parameters.AddWithValue("@Duracion", Pelicula.Duracion);
                    command.Parameters.AddWithValue("@Categoria", Pelicula.Categoria);
                    command.Parameters.AddWithValue("@Director", Pelicula.Director);
                    command.Parameters.AddWithValue("@Anio", Pelicula.Anio);
                    command.Parameters.AddWithValue("@ImagenURL", Pelicula.ImagenURL);
                    command.Parameters.AddWithValue("@Puntuacion", Pelicula.Puntuacion);



                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Peliculas pelicula)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Peliculas SET titulo = @Titulo, sinopsis = @Sinopsis, duracion = @Duracion, categoria = @Categoria, director = @Director, anio = @Anio, imagenURL = @ImagenURL, puntuacion = @Puntuacion WHERE idPelicula = @IdPelicula";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdPelicula", pelicula.IdPelicula);
                    command.Parameters.AddWithValue("@Titulo", pelicula.Titulo);
                    command.Parameters.AddWithValue("@Sinopsis", pelicula.Sinopsis);
                    command.Parameters.AddWithValue("@Duracion", pelicula.Duracion);
                    command.Parameters.AddWithValue("@Categoria", pelicula.Categoria);
                    command.Parameters.AddWithValue("@Director", pelicula.Director);
                    command.Parameters.AddWithValue("@Anio", pelicula.Anio);
                    command.Parameters.AddWithValue("@ImagenURL", pelicula.ImagenURL);
                    command.Parameters.AddWithValue("@Puntuacion", pelicula.Puntuacion);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Peliculas WHERE idPelicula = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InicializarDatosAsync()
        {


        }

    }
}