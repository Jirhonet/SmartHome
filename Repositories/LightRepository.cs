using Dapper;
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

        public async Task<IEnumerable<Light>> GetAsync(CancellationToken ct = default)
        {
            const string sql =
                """
                SELECT
                    l.*
                FROM Light l
                """;

            await using SqlConnection connection = DbContext.GetConnection();
            return await connection.QueryAsync<Light>(sql);
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

            await using SqlConnection connection = DbContext.GetConnection();
            return await connection.QueryFirstOrDefaultAsync<Light>(sql, param);
        }

        public async Task UpdateAsync(Light light, CancellationToken ct = default)
        {
            const string sql =
                """
                UPDATE Light
                SET IsOn = @IsOn,
                    Brightness = @Brightness
                WHERE Id = @Id
                """;

            await using SqlConnection connection = DbContext.GetConnection();
            await connection.ExecuteAsync(sql, light);
        }
    }
}
