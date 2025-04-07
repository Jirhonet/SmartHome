using Microsoft.Data.SqlClient;
using SmartHome.Models;

namespace SmartHome.Repositories
{
    public sealed class RoomRepository : RepositoryBase
    {
        public RoomRepository(DbContext dbContext)
            : base(dbContext)
        {
            //
        }

        public async Task<List<Room>> GetAsync(string search = null, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    r.*
                FROM Room r
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            search ??= string.Empty;
            command.Parameters.AddWithValue("@Search", search);
            using SqlDataReader reader = await command.ExecuteReaderAsync(ct);
            var speakers = new List<Room>();
            while (await reader.ReadAsync(ct))
            {
                speakers.Add(new Room
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                });
            }
            return speakers;
        }

        public async Task<Room> GetByIdAsync(int id, CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    r.*
                FROM Room r
                WHERE r.Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = await command.ExecuteReaderAsync(ct);
            if (await reader.ReadAsync(ct))
            {
                return new Room
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                };
            }
            throw new Exception("Speaker not found");
        }

        public async Task InsertAsync(Room room, CancellationToken ct = default)
        {
            const string sql =
                """
                INSERT INTO Room (Name)
                VALUES (@Name);
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", room.Name);
            await command.ExecuteNonQueryAsync(ct);
        }

        public async Task UpdateAsync(Room room, CancellationToken ct = default)
        {
            const string sql =
                """
                UPDATE Room
                SET [Name] = @Name
                WHERE Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", room.Id);
            command.Parameters.AddWithValue("@Name", room.Name);
            await command.ExecuteNonQueryAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            const string sql =
                """
                DELETE FROM Room
                WHERE Id = @Id
                """;

            await using SqlConnection connection = await DbContext.GetConnection(ct);
            using SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            await command.ExecuteNonQueryAsync(ct);
        }
    }
}