using System.Data;
using Microsoft.Data.SqlClient;

namespace SmartHome.Repositories
{
    public sealed class DbContext : IDisposable
    {
        private readonly IConfiguration configuration;
        private SqlConnection _connection;

        public DbContext(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public SqlConnection GetConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                string connectionString = configuration.GetConnectionString("Default");
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
