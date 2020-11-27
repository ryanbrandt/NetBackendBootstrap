using System.Data;
using MySql.Data.MySqlClient;
using NetBackendBootstrap.Interface;

namespace NetBackendBootstrap.Data
{
    /// <summary>
    /// MySQL implementation of IDatabaseConnectionFactory
    /// Creates MySQL database connections
    /// </summary>
    public class MySqlConnectionFactory : IDatabaseConnectionFactory
    {
        public string ConnectionString { get; }

        public MySqlConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
