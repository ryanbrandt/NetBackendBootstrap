using System.Data;

namespace NetBackendBootstrap.Interface
{
    /// <summary>
    /// Interface used to abstract the database implementation in classes implementing IDatabaseHandler
    /// This allows us to implement IDatabaseHandler without concern for a specific SQL server
    /// </summary>
    public interface IDatabaseConnectionFactory
    {
        string ConnectionString { get; }

        IDbConnection CreateConnection();
    }
}
