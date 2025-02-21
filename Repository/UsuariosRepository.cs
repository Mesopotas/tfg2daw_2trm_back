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
        public async Task<UserDTOOut> GetUserFromCredentialsAsync(LoginDto login)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string userQuery = "SELECT IdUsuario, Nombre, Apellidos, Contrasenia, IdRol FROM Usuarios WHERE Email = @Email";
                using (var command = new SqlCommand(userQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", login.Email);

                    int idUsuario;
                    string nombre, apellidos, contrasenia;
                    int idRol;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                        {
                            return null;
                        }

                        idUsuario = reader.GetInt32(0);
                        nombre = reader.GetString(1);
                        apellidos = reader.GetString(2);
                        contrasenia = reader.GetString(3);
                        idRol = reader.GetInt32(4);
                    }

                    if (contrasenia != login.Contrasenia)
                    {
                        return null;
                    }

                    string rolQuery = "SELECT Nombre FROM Roles WHERE IdRol = @IdRol"; // sentencia para el nombre del rol en su tabla correspondiente
                    using (var rolCommand = new SqlCommand(rolQuery, connection))
                    {
                        rolCommand.Parameters.AddWithValue("@IdRol", idRol);

                        var rol = await rolCommand.ExecuteScalarAsync();
                        if (rol == null)
                        {
                            return null;
                        }

                        return new UserDTOOut
                        {
                            IdUsuario = idUsuario,
                            Nombre = nombre,
                            Apellidos = apellidos,
                            Email = login.Email,
                            Rol = rol.ToString()
                        };
                    }
                }
            }
        }
        public async Task<UserDTOOut> AddUserFromCredentialsAsync(RegisterDTO register)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Verificar si el email ya existe
                string checkEmailQuery = "SELECT Email FROM Usuarios WHERE Email = @Email";
                using (var comprobarEmail = new SqlCommand(checkEmailQuery, connection))
                {
                    comprobarEmail.Parameters.AddWithValue("@Email", register.Email);

                    var emailExiste = (int)await comprobarEmail.ExecuteScalarAsync() > 0;
                    if (emailExiste)
                    {
                        return null; // El email ya está registrado
                    }
                }

                // Insertar el nuevo usuario
                string insertUserQuery = "INSERT INTO Usuarios (Nombre, Apellidos, Email, Contrasenia) " +
                                         "VALUES (@Nombre, @Apellidos, @Email, @Contrasenia); SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(insertUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", register.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", register.Apellidos);
                    command.Parameters.AddWithValue("@Email", register.Email);
                    command.Parameters.AddWithValue("@Contrasenia", register.Contrasenia);

                    var nuevoIdUsuario = await command.ExecuteScalarAsync(); // Obtiene el IdUsuario del nuevo registro insertado

                    if (nuevoIdUsuario == null)
                    {
                        return null; // Error al insertar el nuevo usuario en la BBDD
                    }

                    return new UserDTOOut
                    {
                        IdUsuario = Convert.ToInt32(nuevoIdUsuario),
                        Nombre = register.Nombre,
                        Apellidos = register.Apellidos,
                        Email = register.Email,
                    };
                }
            }
        }

    }
}