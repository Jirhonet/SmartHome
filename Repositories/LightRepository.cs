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

        public async Task<List<Light>> GetAsync(string search = null, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    l.*,
                    r.Id AS RoomId,
                    r.[Name] AS RoomName
                FROM Light l
                LEFT JOIN Room r ON r.Id = l.RoomId
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
                    RoomId = reader.IsDBNull(reader.GetOrdinal("RoomId")) ? null : reader.GetInt32(reader.GetOrdinal("RoomId")),
                    RoomName = reader.IsDBNull(reader.GetOrdinal("RoomName")) ? null : reader.GetString(reader.GetOrdinal("RoomName")),
                });
            }
            return lights;
        }

        public async Task<Light> GetByIdAsync(int id, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    l.*,
                    r.Id AS RoomId,
                    r.[Name] AS RoomName
                FROM Light l
                LEFT JOIN Room r ON r.Id = l.RoomId
                WHERE l.Id = @Id
                """;

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
                    RoomId = reader.IsDBNull(reader.GetOrdinal("RoomId")) ? null : reader.GetInt32(reader.GetOrdinal("RoomId")),
                    RoomName = reader.IsDBNull(reader.GetOrdinal("RoomName")) ? null : reader.GetString(reader.GetOrdinal("RoomName")),
                };
            }
            throw new Exception("Light not found");
        }

        public async Task<List<Light>> GetByRoomIdAsync(int roomId, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    l.*,
                    r.Id AS RoomId,
                    r.[Name] AS RoomName
                FROM Light l
                LEFT JOIN Room r ON r.Id = l.RoomId
                WHERE l.RoomId = @RoomId
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@RoomId", roomId);
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
                    RoomId = reader.IsDBNull(reader.GetOrdinal("RoomId")) ? null : reader.GetInt32(reader.GetOrdinal("RoomId")),
                    RoomName = reader.IsDBNull(reader.GetOrdinal("RoomName")) ? null : reader.GetString(reader.GetOrdinal("RoomName")),
                });
            }
            return lights;
        }

        public async Task InsertAsync(Light light, CancellationToken ct = default)
        {
            const string sql =
                """
                INSERT INTO Light (Name, IsOn, Brightness, RoomId)
                VALUES (@Name, @IsOn, @Brightness, @RoomId);
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", light.Name);
            command.Parameters.AddWithValue("@IsOn", light.IsOn);
            command.Parameters.AddWithValue("@Brightness", light.Brightness);
            command.Parameters.AddWithValue("@RoomId", (object)light.RoomId ?? DBNull.Value);
            await command.ExecuteNonQueryAsync(ct);
        }

        public async Task UpdateAsync(Light light, CancellationToken ct = default)
        {
            const string sql =
                """
                UPDATE Light
                SET [Name] = @Name,
                    IsOn = @IsOn,
                    Brightness = @Brightness,
                    RoomId = @RoomId
                WHERE Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", light.Id);
            command.Parameters.AddWithValue("@Name", light.Name);
            command.Parameters.AddWithValue("@IsOn", light.IsOn);
            command.Parameters.AddWithValue("@Brightness", light.Brightness);
            command.Parameters.AddWithValue("@RoomId", (object)light.RoomId ?? DBNull.Value);
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
