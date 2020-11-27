using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using NetBackendBootstrap.Interface;
using NetBackendBootstrap.Model;
using NetBackendBootstrap.Utils;

namespace NetBackendBootstrap.Data
{
    /// <summary>
    /// Basic implementation of IDatabaseHandler which can work with multiple SQL servers
    /// </summary>
    public class DatabaseHandler : IDatabaseHandler
    {
        protected const string LOG_SOURCE = "database handler";

        /// <summary>
        /// IDatabaseConnectionFactory used to establish database connections
        /// </summary>
        public IDatabaseConnectionFactory ConnectionFactory { get; }

        public DatabaseHandler(string connectionString, IDatabaseConnectionFactory connectionFactory = null)
        {
            if (connectionFactory != null)
            {
                ConnectionFactory = connectionFactory;
            }
            else
            {
                ConnectionFactory = new MsSqlConnectionFactory(connectionString);
            }
        }

        public DatabaseHandler(IDatabaseConnectionFactory connectionFactory)
            : this(null, connectionFactory) { }

        public DatabaseHandler(string connectionString)
            : this(connectionString, null) { }

        public DatabaseHandler()
            : this(null, null) { }

        /// <summary>
        /// Managed Execute method. Creates and diposes database connections upon completion.
        /// Should only be used when we want to execute a single statement.
        /// </summary>
        public async Task<ExecuteResult> Execute(string sql, object sqlArgs = null)
        {
            var result = new ExecuteResult()
            {
                Success = true
            };
            try
            {
                using (IDbConnection connection = ConnectionFactory.CreateConnection())
                {
                    Logger.Log(LOG_SOURCE, sql);
                    var rowsAffected = await connection.ExecuteAsync(sql, sqlArgs);
                    result.RowsAffected = rowsAffected;
                }
            }
            catch (Exception e)
            {
                Logger.Log(LOG_SOURCE, $"Failed execute {e}");
                result.Success = false;
                result.Error = e;
            }

            return result;
        }

        /// <summary>
        /// Unmanaged Execute method. Accepts an IDBConnection and leaves the responsibility
        /// of ensuring the connection is open and subsequently disposed of to the caller.
        /// Should only be used when we want to execute multiple statements.
        /// </summary>
        public async Task<ExecuteResult> Execute(IDbConnection connection, string sql, object sqlArgs = null)
        {
            var result = new ExecuteResult()
            {
                Success = true
            };
            try
            {
                Logger.Log(LOG_SOURCE, sql);
                var rowsAffected  = await connection.ExecuteAsync(sql, sqlArgs);
                result.RowsAffected = rowsAffected;
            }
            catch (Exception e)
            {
                Logger.Log(LOG_SOURCE, $"Failed execute {e}");
                result.Success = false;
                result.Error = e;
            }

            return result;
        }

        /// <summary>
        /// Managed Query method. Creates and diposes database connections upon completion.
        /// Should only be used when we want to complete a single query.
        /// </summary>
        public async Task<QueryResult<T>> Query<T>(string sql, object sqlArgs = null)
        {
            var result = new QueryResult<T>()
            {
                Success = true
            };
            try
            {
                using (IDbConnection connection = ConnectionFactory.CreateConnection())
                {
                    Logger.Log(LOG_SOURCE, sql);
                    var data = await connection.QueryAsync<T>(sql, sqlArgs);
                    result.Data = data.AsList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(LOG_SOURCE, $"Failed query {e}");
                result.Success = false;
                result.Error = e;
            }

            return result;
        }

        /// <summary>
        /// Unmanaged Query method. Accepts an IDBConnection and leaves the responsibility
        /// of ensuring the connection is open and subsequently disposed of to the caller.
        /// Should only be used when we want to complete multiple queries.
        /// </summary>
        public async Task<QueryResult<T>> Query<T>(IDbConnection connection, string sql, object sqlArgs = null)
        {
            var result = new QueryResult<T>()
            {
                Success = true
            };
            try
            {
                Logger.Log(LOG_SOURCE, sql);
                var data = await connection.QueryAsync<T>(sql, sqlArgs);
                result.Data = data.AsList();
            }
            catch (Exception e)
            {
                Logger.Log(LOG_SOURCE, $"Failed query {e}");
                result.Success = false;
                result.Error = e;
            }

            return result;
        }
    }
}
