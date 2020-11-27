using System.Data;
using Microsoft.Data.Sqlite;
using NetBackendBootstrap.Interface;

namespace NetBackendBootstrap.Data
{
    /// <summary>
    /// SQLite implementation of IDatabaseConnectionFactory
    /// Creates SQLite database connections
    /// </summary>
    public class SqliteConnectionFactory : IDatabaseConnectionFactory
    {
        public string ConnectionString { get; }

        public SqliteConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}
