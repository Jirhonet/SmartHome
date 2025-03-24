using Microsoft.Data.SqlClient;
using SmartHome.Models;

namespace SmartHome.Repositories
{
    public sealed class LightRepository : RepositoryBase
    {
        public LightRepository(DbContext dbContext)
            : base(dbContext)
        {
            //
        }

        public async Task<IEnumerable<Light>> GetAsync(string search = null, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    l.*
                FROM Light l
                WHERE l.[Name] LIKE '%' + @Search + '%'
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            search ??= string.Empty;
            command.Parameters.AddWithValue("@Search", search);
            using SqlDataReader reader = await command.ExecuteReaderAsync(ct);
            var lights = new List<Light>();
            while (await reader.ReadAsync(ct))
            {
                lights.Add(new Light
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    IsOn = reader.GetBoolean(reader.GetOrdinal("IsOn")),
                    Brightness = reader.GetInt32(reader.GetOrdinal("Brightness")),
                });
            }
            return lights;
        }

        public async Task<Light> GetByIdAsync(int id, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    l.*
                FROM Light l
                WHERE l.Id = @Id
                """;

            var param = new
            {
                Id = id,
            };

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = await command.ExecuteReaderAsync(ct);
            if (await reader.ReadAsync(ct))
            {
                return new Light
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    IsOn = reader.GetBoolean(reader.GetOrdinal("IsOn")),
                    Brightness = reader.GetInt32(reader.GetOrdinal("Brightness")),
                };
            }
            throw new Exception("Light not found");
        }

        public async Task InsertAsync(Light light, CancellationToken ct = default)
        {
            const string sql =
                """
                INSERT INTO Light (Name, IsOn, Brightness)
                VALUES (@Name, @IsOn, @Brightness);
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", light.Name);
            command.Parameters.AddWithValue("@IsOn", light.IsOn);
            command.Parameters.AddWithValue("@Brightness", light.Brightness);
            await command.ExecuteNonQueryAsync(ct);
        }

        public async Task UpdateAsync(Light light, CancellationToken ct = default)
        {
            const string sql =
                """
                UPDATE Light
                SET [Name] = @Name,
                    IsOn = @IsOn,
                    Brightness = @Brightness
                WHERE Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", light.Id);
            command.Parameters.AddWithValue("@Name", light.Name);
            command.Parameters.AddWithValue("@IsOn", light.IsOn);
            command.Parameters.AddWithValue("@Brightness", light.Brightness);
            await command.ExecuteNonQueryAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            const string sql =
                """
                DELETE FROM Light
                WHERE Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync(ct);
        }
    }
}
