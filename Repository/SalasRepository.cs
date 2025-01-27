using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Repositories
{
    public class SalasRepository : ISalasRepository
    {
        private readonly string _connectionString;

        public SalasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Salas>> GetAllAsync()
        {
            var salas = new List<Salas>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idSala, nombre, capacidad FROM Salas";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var sala = new Salas(
                                id: reader.GetInt32(0),
                                nombre: reader.GetString(1),
                                capacidad: reader.GetInt32(2),
                                asientos: new List<Asientos>()
                            );

                            salas.Add(sala);
                        }
                    }
                }
            }
            return salas;
        }

        public async Task<Salas> GetByIdAsync(int id)
        {
            Salas sala = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT idSala, nombre, capacidad FROM Salas WHERE idSala = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sala = new Salas(
                                id: reader.GetInt32(0),
                                nombre: reader.GetString(1),
                                capacidad: reader.GetInt32(2),
                                asientos: new List<Asientos>()
                            );
                        }
                    }
                }
            }
            return sala;
        }
        public async Task AddAsync(Salas sala)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string insertSala = @"
            INSERT INTO Salas (nombre, capacidad) VALUES (@Nombre, @Capacidad);
            SELECT CAST(SCOPE_IDENTITY() AS INT);"; // devolverá el valor del ID de la sala creada, para asi poder usarla en el insert de los asientos, con el cast transformará su tipo directamente a int para poder utilizarlo

                int idSala;

                using (var command = new SqlCommand(insertSala, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", sala.Nombre);
                    command.Parameters.AddWithValue("@Capacidad", sala.Capacidad);

                    // Usamos ExecuteReader para obtener el ID
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            idSala = reader.GetInt32(0);
                        }
                        else
                        {
                            throw new Exception("Error al obtener el ID de la sala de referencia");
                        }
                    }
                }

                // Insertar asientos con un bucle for en base a la capacidad de la sala
                for (int i = 1; i <= sala.Capacidad; i++)
                {
                    string insertarAsientosSala = @"
                INSERT INTO Asientos (idSala, numAsiento, estado) VALUES (@IdSala, @NumAsiento, 1)";

                    using (var command = new SqlCommand(insertarAsientosSala, connection))
                    {
                        command.Parameters.AddWithValue("@IdSala", idSala);
                        command.Parameters.AddWithValue("@NumAsiento", i);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task UpdateAsync(Salas sala)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    UPDATE Salas 
                    SET nombre = @Nombre, capacidad = @Capacidad 
                    WHERE idSala = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", sala.Id);
                    command.Parameters.AddWithValue("@Nombre", sala.Nombre);
                    command.Parameters.AddWithValue("@Capacidad", sala.Capacidad);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
     public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Salas WHERE idSala = @Id";
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