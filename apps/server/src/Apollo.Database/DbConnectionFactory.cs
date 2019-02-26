using System.Data;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MySql.Data.MySqlClient;

namespace Apollo.Database
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("mysql");
        }

        public async Task<IDbConnection> OpenAsync()
        {
            var connection = new MySqlConnection(_connectionString);

            await connection.OpenAsync();

            return connection;
        }
    }
}
