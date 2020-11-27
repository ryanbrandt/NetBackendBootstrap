using System.Data;
using NetBackendBootstrap.Interface;
using Npgsql;

namespace NetBackendBootstrap.Data
{
    /// <summary>
    /// PostgreSQL implementation of IDatabaseConnectionFactory
    /// Creates PostgreSQL database connections
    /// </summary>
    public class PostgreSqlConnection : IDatabaseConnectionFactory
    {
        public string ConnectionString { get; }

        public PostgreSqlConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }
    }
}
