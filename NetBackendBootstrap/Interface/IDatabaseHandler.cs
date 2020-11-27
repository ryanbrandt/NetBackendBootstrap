using System.Threading.Tasks;
using System.Data;
using NetBackendBootstrap.Model;

namespace NetBackendBootstrap.Interface
{
    /// <summary>
    /// Interface to allow abstracting the implementation of database handlers/helpers
    /// </summary>
    public interface IDatabaseHandler
    {
        Task<QueryResult<T>> Query<T>(string sql, object sqlArgs = null);

        Task<QueryResult<T>> Query<T>(IDbConnection connection, string sql, object sqlArgs = null);

        Task<ExecuteResult> Execute(string sql, object sqlArgs = null);

        Task<ExecuteResult> Execute(IDbConnection connection, string sql, object sqlArgs = null);
    }
}
