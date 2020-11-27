using System.Data;
using NetBackendBootstrap.Interface;
using Microsoft.Data.SqlClient;

namespace NetBackendBootstrap.Data
{
    /// <summary>
    /// MS SQL implementation of IDatabaseConnectionFactory
    /// Creates MS SQL database connections
    /// </summary>
    public class MsSqlConnectionFactory : IDatabaseConnectionFactory
    {
        public string ConnectionString { get; }

        public MsSqlConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
