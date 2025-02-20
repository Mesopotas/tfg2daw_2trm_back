using Microsoft.Data.SqlClient;
using Models;
using CoWorking.DTO;

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

                string query = "SELECT IdUsuario, Nombre, Apellidos, Email, Contrasenia, FechaRegistro, IdRol FROM Usuarios";
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
                                Email = reader.GetString(3),
                                Contrasenia = reader.GetString(4),
                                FechaRegistro = reader.GetDateTime(5),
                                IdRol = reader.GetInt32(6)

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

                string query = "SELECT IdUsuario, Nombre, Apellidos, Email, Contrasenia, FechaRegistro, IdRol FROM Usuarios WHERE idUsuario = @Id";
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
                                Email = reader.GetString(3),
                                Contrasenia = reader.GetString(4),
                                FechaRegistro = reader.GetDateTime(5),
                                IdRol = reader.GetInt32(6)

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

                string query = "INSERT INTO Usuarios (Nombre, Apellidos, Email, Contrasenia, FechaRegistro, IdRol) VALUES (@Nombre, @Apellidos, @Email, @Contrasenia, @FechaRegistro, @IdRol)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@Email", usuario.Email.ToLower());
                    command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                    command.Parameters.AddWithValue("@FechaRegistro", DateTime.Now); // dado que es un nuevo registro a la bbdd y por tanto nuevo usuario, su fecha de unión será siempre la fecha actual de ese momento  
                    command.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuarios usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // La columna FechaRegistro no está incluida ya que no debe ser modificada
                string query = "UPDATE Usuarios SET nombre = @Nombre, apellidos = @Apellidos,  email = @Email, contrasenia = @Contrasenia, idRol = @IdRol WHERE idUsuario = @IdUsuario";
                // si el idRol asignado no existe dará error (Microsoft.Data.SqlClient.SqlException (0x80131904): The INSERT statement conflicted with the FOREIGN KEY constraint "FK__Usuarios__IdRol__276EDEB3". The conflict occurred in database "CoworkingDB", table "dbo.Roles", column 'IdRol'.)

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
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

                string query = "DELETE FROM Usuarios WHERE idUsuario = @IdUsuario";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }




        public async Task<List<UsuarioClienteDTO>> GetClientesByEmailAsync(string Email)
        {
            var clientes = new List<UsuarioClienteDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Nombre, Apellidos, Email, Contrasenia FROM Usuarios WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email".ToLower(), Email);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var cliente = new UsuarioClienteDTO
                            {
                                Nombre = reader.GetString(0),
                                Apellidos = reader.GetString(1),
                                Email = reader.GetString(2),
                                Contrasenia = reader.GetString(3)
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }
            return clientes;
        }
          public async Task<List<UsuarioClienteDTO>> ComprobarCredencialesAsync(string Email, string Contrasenia)
        {
            var clientes = new List<UsuarioClienteDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Nombre, Apellidos, Email FROM Usuarios WHERE Email = @Email AND Contrasenia = @Contrasenia";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email".ToLower(), Email);
                    command.Parameters.AddWithValue("@Contrasenia", Contrasenia);


                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var cliente = new UsuarioClienteDTO
                            {
                                Nombre = reader.GetString(0),
                                Apellidos = reader.GetString(1),
                                Email = reader.GetString(2)
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }
            return clientes;
        }
    }
}