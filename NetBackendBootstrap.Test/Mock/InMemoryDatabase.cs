using System.Data;
using Microsoft.Data.Sqlite;

namespace NetBackendBootstrap.test.Mock
{
    public class InMemoryDatabase
    {
        private readonly SqliteConnection _connection;

        public InMemoryDatabase()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
        }

        public SqliteConnection OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }

        public void SetupTable(string tableCreationSql, params string[] insertSql)
        {
            using (IDbCommand command = _connection.CreateCommand())
            {
                command.CommandText = tableCreationSql;
                command.ExecuteNonQuery();

                foreach (var insert in insertSql)
                {
                    command.CommandText = insert;
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
