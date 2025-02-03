using Microsoft.Data.SqlClient;
using Models;

namespace Cinema.Repositories
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

                string query = "SELECT idUsuario, dni, nombre, apellidos, email, contrasenia, fechaRegistro FROM Usuarios";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new Usuarios
                            {
                                IdUsuario = reader.GetInt32(0),
                                DNI = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Apellidos = reader.GetString(3),
                                Email = reader.GetString(4),
                                Contrasenia = reader.GetString(5),
                                FechaRegistro = reader.GetDateTime(6)
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

                string query = "SELECT idUsuario, dni, nombre, apellidos, email, contrasenia, fechaRegistro FROM Usuarios WHERE idUsuario = @Id";
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
                                DNI = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Apellidos = reader.GetString(3),
                                Email = reader.GetString(4),
                                Contrasenia = reader.GetString(5),
                                FechaRegistro = reader.GetDateTime(6)
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

                string query = "INSERT INTO Usuarios (dni, nombre, apellidos, email, contrasenia, fechaRegistro) VALUES (@DNI, @Nombre, @Apellidos, @Email, @Contrasenia, @FechaRegistro)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DNI", usuario.DNI);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                    command.Parameters.AddWithValue("@FechaRegistro", usuario.FechaRegistro);


                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuarios usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Usuarios SET dni = @DNI, nombre = @Nombre, apellidos = @apellidos, email = @Email, contrasenia = @Contrasenia, fechaRegistro = @FechaRegistro WHERE idUsuario = @IdUsuario";
                using (var command = new SqlCommand(query, connection))
                {
                                     
                    command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@DNI", usuario.DNI);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                    command.Parameters.AddWithValue("@FechaRegistro", usuario.FechaRegistro);

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

        public async Task InicializarDatosAsync()
        {


        }

    }
}