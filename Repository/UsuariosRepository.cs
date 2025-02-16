using Microsoft.Data.SqlClient;
using Models;

namespace CoWorking.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly string _connectionString;

        public UsuariosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Usuarios>> GetAllAsync()
        {
            var usuarios = new List<Usuarios>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdUsuario, Nombre, Apellidos, DNI, Email, Contrasenia, Telefono, FechaRegistro, IdRol FROM Usuarios";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new Usuarios
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellidos = reader.GetString(2),
                                 DNI = reader.GetString(3),
                                Email = reader.GetString(4),
                                Contrasenia = reader.GetString(5),
                                Telefono = reader.GetString(6),
                                FechaRegistro = reader.GetDateTime(7),
                                IdRol = reader.GetInt32(8)

                            };

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }

        public async Task<Usuarios> GetByIdAsync(int id)
        {
            Usuarios usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT IdUsuario, Nombre, Apellidos, DNI, Email, Contrasenia, Telefono, FechaRegistro, IdRol FROM Usuarios WHERE idUsuario = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuarios
                            {
                                IdUsuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellidos = reader.GetString(2),
                                 DNI = reader.GetString(3),
                                Email = reader.GetString(4),
                                Contrasenia = reader.GetString(5),
                                Telefono = reader.GetString(6),
                                FechaRegistro = reader.GetDateTime(7),
                                IdRol = reader.GetInt32(8)

                            };

                        }
                    }
                }
            }
            return usuario;
        }

        public async Task AddAsync(Usuarios usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Usuarios (nombre, apellidos, dni, email, contrasenia, telefono, idRol) VALUES (@nombre, @apellidos, @dni, @email, @contrasenia, @telefono, @idRol)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@dni", usuario.DNI);
                    command.Parameters.AddWithValue("@email", usuario.Email);
                    command.Parameters.AddWithValue("@contrasenia", usuario.Contrasenia);
                    command.Parameters.AddWithValue("@telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@idRol", usuario.IdRol);



                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuarios usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Usuarios SET nombre = @Nombre, apellidos = @apellidos,  dni = @DNI, email = @Email, contrasenia = @Contrasenia, @telefono = telefono, idRol = @idRol WHERE idUsuario = @IdUsuario"; // FECHA REGISTRO NO EDITABLE YA QUE DEBE SER ESTATICA E INMOVIL
                // si el idol asignado no existe dará error (Microsoft.Data.SqlClient.SqlException (0x80131904): The INSERT statement conflicted with the FOREIGN KEY constraint "FK__Usuarios__IdRol__276EDEB3". The conflict occurred in database "CoworkingDB", table "dbo.Roles", column 'IdRol'.)
                using (var command = new SqlCommand(query, connection))
                {
                                     
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@DNI", usuario.DNI);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                    command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@IdRol", usuario.IdRol);


                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Usuarios WHERE idUsuario = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

  
    }
}