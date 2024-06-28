using GakkoHorizontalSlice.Model;
using System.Data.SqlClient;

namespace GakkoHorizontalSlice.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string _connectionString;

        public AnimalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Animal>> GetAnimalsAsync(string orderBy)
        {
            var animals = new List<Animal>();
            var validColumns = new List<string> { "name", "description", "category", "area" };
            orderBy = validColumns.Contains(orderBy.ToLower()) ? orderBy : "name";

            var query = $"SELECT * FROM Animals ORDER BY {orderBy}";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            animals.Add(new Animal
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Description = reader["Description"] != DBNull.Value ? (string)reader["Description"] : null,
                                Category = reader["Category"] != DBNull.Value ? (string)reader["Category"] : null,
                                Area = reader["Area"] != DBNull.Value ? (string)reader["Area"] : null
                            });
                        }
                    }
                }
            }

            return animals;
        }

        public async Task AddAnimalAsync(Animal animal)
        {
            var query = "INSERT INTO Animals (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", animal.Name);
                    command.Parameters.AddWithValue("@Description", animal.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Category", animal.Category ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Area", animal.Area ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAnimalAsync(int id, Animal animal)
        {
            var query = "UPDATE Animals SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", animal.Name);
                    command.Parameters.AddWithValue("@Description", animal.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Category", animal.Category ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Area", animal.Area ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAnimalAsync(int id)
        {
            var query = "DELETE FROM Animals WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
