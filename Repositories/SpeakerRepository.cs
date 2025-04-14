using Microsoft.Data.SqlClient;
using SmartHome.Enums;
using SmartHome.Models;

namespace SmartHome.Repositories
{
    public sealed class SpeakerRepository : RepositoryBase
    {
        public SpeakerRepository(DbContext dbContext)
            : base(dbContext)
        {
            //
        }

        public async Task<List<Speaker>> GetAsync(string search = null, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    s.*,
                    r.Id AS RoomId,
                    r.[Name] AS RoomName
                FROM Speaker s
                LEFT JOIN Room r ON r.Id = s.RoomId
                WHERE s.[Name] LIKE '%' + @Search + '%'
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            search ??= string.Empty;
            command.Parameters.AddWithValue("@Search", search);
            using SqlDataReader reader = await command.ExecuteReaderAsync(ct);
            var speakers = new List<Speaker>();
            while (await reader.ReadAsync(ct))
            {
                speakers.Add(new Speaker
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    State = (SpeakerState)reader.GetInt32(reader.GetOrdinal("State")),
                    Volume = reader.GetInt32(reader.GetOrdinal("Volume")),
                    RoomId = reader.IsDBNull(reader.GetOrdinal("RoomId")) ? null : reader.GetInt32(reader.GetOrdinal("RoomId")),
                    RoomName = reader.IsDBNull(reader.GetOrdinal("RoomName")) ? null : reader.GetString(reader.GetOrdinal("RoomName")),
                });
            }
            return speakers;
        }

        public async Task<Speaker> GetByIdAsync(int id, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    s.*,
                    r.Id AS RoomId,
                    r.[Name] AS RoomName
                FROM Speaker s
                LEFT JOIN Room r ON r.Id = s.RoomId
                WHERE s.Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = await command.ExecuteReaderAsync(ct);
            if (await reader.ReadAsync(ct))
            {
                return new Speaker
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    State = (SpeakerState)reader.GetInt32(reader.GetOrdinal("State")),
                    Volume = reader.GetInt32(reader.GetOrdinal("Volume")),
                    RoomId = reader.IsDBNull(reader.GetOrdinal("RoomId")) ? null : reader.GetInt32(reader.GetOrdinal("RoomId")),
                    RoomName = reader.IsDBNull(reader.GetOrdinal("RoomName")) ? null : reader.GetString(reader.GetOrdinal("RoomName")),
                };
            }
            throw new Exception("Speaker not found");
        }

        public async Task<List<Speaker>> GetByRoomIdAsync(int roomId, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    s.*,
                    r.Id AS RoomId,
                    r.[Name] AS RoomName
                FROM Speaker s
                LEFT JOIN Room r ON r.Id = s.RoomId
                WHERE s.RoomId = @RoomId
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@RoomId", roomId);
            using SqlDataReader reader = await command.ExecuteReaderAsync(ct);
            var speakers = new List<Speaker>();
            while (await reader.ReadAsync(ct))
            {
                speakers.Add(new Speaker
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    State = (SpeakerState)reader.GetInt32(reader.GetOrdinal("State")),
                    Volume = reader.GetInt32(reader.GetOrdinal("Volume")),
                    RoomId = reader.IsDBNull(reader.GetOrdinal("RoomId")) ? null : reader.GetInt32(reader.GetOrdinal("RoomId")),
                    RoomName = reader.IsDBNull(reader.GetOrdinal("RoomName")) ? null : reader.GetString(reader.GetOrdinal("RoomName")),
                });
            }
            return speakers;
        }

        public async Task InsertAsync(Speaker speaker, CancellationToken ct = default)
        {
            const string sql =
                """
                INSERT INTO Speaker (Name, State, Volume, RoomId)
                VALUES (@Name, @State, @Volume, @RoomId);
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", speaker.Name);
            command.Parameters.AddWithValue("@State", speaker.State);
            command.Parameters.AddWithValue("@Volume", speaker.Volume);
            command.Parameters.AddWithValue("@RoomId", (object)speaker.RoomId ?? DBNull.Value);
            await command.ExecuteNonQueryAsync(ct);
        }

        public async Task UpdateAsync(Speaker speaker, CancellationToken ct = default)
        {
            const string sql =
                """
                UPDATE Speaker
                SET [Name] = @Name,
                    State = @State,
                    Volume = @Volume,
                    RoomId = @RoomId
                WHERE Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", speaker.Id);
            command.Parameters.AddWithValue("@Name", speaker.Name);
            command.Parameters.AddWithValue("@State", speaker.State);
            command.Parameters.AddWithValue("@Volume", speaker.Volume);
            command.Parameters.AddWithValue("@RoomId", (object)speaker.RoomId ?? DBNull.Value);
            await command.ExecuteNonQueryAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            const string sql =
                """
                DELETE FROM Speaker
                WHERE Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync(ct);
        }
    }
}