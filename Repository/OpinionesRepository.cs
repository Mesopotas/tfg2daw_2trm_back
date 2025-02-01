using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Repositories
{
    public class OpinionesRepository : IOpinionesRepository
    {
        private readonly string _connectionString;

        public OpinionesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Opiniones>> GetAllAsync()
        {
            var opiniones = new List<Opiniones>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IDOPINION, IDUSUARIO, IDPELICULA, COMENTARIO, FECHACOMENTARIO FROM OPINIONES";
                
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        opiniones.Add(new Opiniones(
                            id: reader.GetInt32(0),
                            idUsuario: reader.GetInt32(1),
                            idPelicula: reader.GetInt32(2),
                            comentario: reader.GetString(3),
                            fechaComentario: reader.GetDateTime(4)
                        ));
                    }
                }
            }
            return opiniones;
        }

        public async Task<Opiniones> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT IDOPINION, IDUSUARIO, IDPELICULA, COMENTARIO, FECHACOMENTARIO FROM OPINIONES WHERE IDOPINION = @Id";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Opiniones(
                                id: reader.GetInt32(0),
                                idUsuario: reader.GetInt32(1),
                                idPelicula: reader.GetInt32(2),
                                comentario: reader.GetString(3),
                                fechaComentario: reader.GetDateTime(4)
                            );
                        }
                    }
                }
            }
            return null;
        }

        public async Task AddAsync(Opiniones opinion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO OPINIONES (IDUSUARIO, IDPELICULA, COMENTARIO, FECHACOMENTARIO) VALUES (@IdUsuario, @IdPelicula, @Comentario, @FechaComentario)";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", opinion.IdUsuario);
                    command.Parameters.AddWithValue("@IdPelicula", opinion.IdPelicula);
                    command.Parameters.AddWithValue("@Comentario", opinion.Comentario);
                    command.Parameters.AddWithValue("@FechaComentario", opinion.FechaComentario);
                    
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Opiniones opinion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE OPINIONES SET IDUSUARIO = @IdUsuario, IDPELICULA = @IdPelicula, COMENTARIO = @Comentario, FECHACOMENTARIO = @FechaComentario WHERE IDOPINION = @IdOpinion";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdOpinion", opinion.Id);
                    command.Parameters.AddWithValue("@IdUsuario", opinion.IdUsuario);
                    command.Parameters.AddWithValue("@IdPelicula", opinion.IdPelicula);
                    command.Parameters.AddWithValue("@Comentario", opinion.Comentario);
                    command.Parameters.AddWithValue("@FechaComentario", opinion.FechaComentario);
                   
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM OPINIONES WHERE IDOPINION = @Id";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
