using System.Collections.Generic;
using System.Data;
using NetBackendBootstrap.Data;
using NetBackendBootstrap.Interface;
using NetBackendBootstrap.test.Mock;
using Moq;
using Xunit;

namespace NetBackendBootstrap.test
{
    public class DatabaseHandlerTest
    {
        private const string MOCK_FIRST_ROW = "Mock";

        private const string MOCK_SECOND_ROW = "Also Mock";

        private void SetupMockDatabase(InMemoryDatabase db)
        {
            var mockCreateTableSql = "CREATE TABLE Mock (id INT PRIMARY KEY, name VARCHAR(30))";
            var firstMockInsertSql = $"INSERT INTO Mock (id, name) VALUES (1, '{MOCK_FIRST_ROW}')";
            var secondMockInsertSql = $"INSERT INTO Mock (id, name) VALUES (2, '{MOCK_SECOND_ROW}')";
            db.SetupTable(mockCreateTableSql, firstMockInsertSql, secondMockInsertSql);
        }

        [Fact]
        public void DatabaseHandler_NoParams_Constructor()
        {
            var db = new DatabaseHandler();

            Assert.NotNull(db);
        }

        [Fact]
        public void DatabaseHandler_IDatabaseConnectionFactory_Constructor()
        {
            var connectionFactory = new MsSqlConnectionFactory(null);
            var db = new DatabaseHandler(connectionFactory);

            Assert.NotNull(db);
            Assert.Equal(connectionFactory, db.ConnectionFactory);
        }

        [Fact]
        public void DatabaseHandler_ConnectionString_Constructor()
        {
            var mockConnectionString = "not a connection string";
            var db = new DatabaseHandler(mockConnectionString);

            Assert.NotNull(db);
            Assert.IsType<MsSqlConnectionFactory>(db.ConnectionFactory);
            Assert.Equal(mockConnectionString, db.ConnectionFactory.ConnectionString);
        }

        [Fact]
        public async void DatabaseHandler_NoSqlArgs_Managed_Execute()
        {
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            var result = await handler.Execute("DELETE FROM Mock WHERE id = 2");

            Assert.True(result.Success);
            Assert.Equal(ConnectionState.Closed, mockConnectionFactory.Object.CreateConnection().State);
            Assert.Equal(1, result.RowsAffected);
        }

        [Fact]
        public async void DatabaseHandler_SqlArgs_Managed_Execute()
        {
            var mockUpdatedName = "New Name";
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            var result = await handler.Execute("UPDATE Mock SET name = @UpdatedName WHERE id = @Id", new { UpdatedName = mockUpdatedName, Id = 2 });

            Assert.True(result.Success);
            Assert.Equal(ConnectionState.Closed, mockConnectionFactory.Object.CreateConnection().State);
            Assert.Equal(1, result.RowsAffected);
        }

        [Fact]
        public async void DatabaseHandler_NoSqlArgs_Unmanaged_Execute()
        {
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            using (var connection = handler.ConnectionFactory.CreateConnection())
            {
                var result = await handler.Execute(connection, "DELETE FROM Mock WHERE id = 2");

                Assert.True(result.Success);
                Assert.Equal(ConnectionState.Open, mockConnectionFactory.Object.CreateConnection().State);
                Assert.Equal(1, result.RowsAffected);
            }
        }

        [Fact]
        public async void DatabaseHandler_SqlArgs_Unmanaged_Execute()
        {
            var mockUpdatedName = "New Name";
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            using (var connection = handler.ConnectionFactory.CreateConnection())
            {
                var result = await handler.Execute(connection, "UPDATE Mock SET name = @UpdatedName WHERE id = @Id", new { UpdatedName = mockUpdatedName, Id = 2 });

                Assert.True(result.Success);
                Assert.Equal(ConnectionState.Open, mockConnectionFactory.Object.CreateConnection().State);
                Assert.Equal(1, result.RowsAffected);
            }
        }

        [Fact]
        public async void DatabaseHandler_NoSqlArgs_Managed_Query()
        {
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            var result = await handler.Query<dynamic>("SELECT * FROM Mock");

            Assert.True(result.Success);
            Assert.Equal(ConnectionState.Closed, mockConnectionFactory.Object.CreateConnection().State);
            Assert.Collection(result.Data, data => Assert.Contains(MOCK_FIRST_ROW, data.name),
                                           data => Assert.Contains(MOCK_SECOND_ROW, data.name));
        }

        [Fact]
        public async void DatabaseHandler_SqlArgs_Managed_Query()
        {
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            var result = await handler.Query<dynamic>("SELECT * FROM Mock WHERE id = @id", new { id = 1 });

            Assert.True(result.Success);
            Assert.Equal(ConnectionState.Closed, mockConnectionFactory.Object.CreateConnection().State);
            Assert.Collection(result.Data, data => Assert.Contains(MOCK_FIRST_ROW, data.name));
            Assert.Collection(result.Data, data => Assert.DoesNotContain(MOCK_SECOND_ROW, data.name));

        }

        [Fact]
        public async void DatabaseHandler_DapperMapping_Managed_Query()
        {
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            var result = await handler.Query<MockTable>("SELECT * FROM Mock");

            Assert.True(result.Success);
            Assert.Equal(ConnectionState.Closed, mockConnectionFactory.Object.CreateConnection().State);
            Assert.IsType<List<MockTable>>(result.Data);
            Assert.Collection(result.Data, data => Assert.Contains(MOCK_FIRST_ROW, data.Name),
                                          data => Assert.Contains(MOCK_SECOND_ROW, data.Name));
        }

        [Fact]
        public async void DatabaseHandler_NoSqlArgs_Unmanaged_Query()
        {
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            using (var connection = handler.ConnectionFactory.CreateConnection())
            {
                var result = await handler.Query<dynamic>(connection, "SELECT * FROM Mock");

                Assert.True(result.Success);
                Assert.Equal(ConnectionState.Open, mockConnectionFactory.Object.CreateConnection().State);
                Assert.Collection(result.Data, data => Assert.Contains(MOCK_FIRST_ROW, data.name),
                                               data => Assert.Contains(MOCK_SECOND_ROW, data.name));
            }
        }

        [Fact]
        public async void DatabaseHandler_SqlArgs_Unmanaged_Query()
        {
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            using (var connection = handler.ConnectionFactory.CreateConnection())
            {
                var result = await handler.Query<dynamic>(connection, "SELECT * FROM Mock WHERE id = @id", new { id = 1 });

                Assert.True(result.Success);
                Assert.Equal(ConnectionState.Open, mockConnectionFactory.Object.CreateConnection().State);
                Assert.Collection(result.Data, data => Assert.Contains(MOCK_FIRST_ROW, data.name));
                Assert.Collection(result.Data, data => Assert.DoesNotContain(MOCK_SECOND_ROW, data.name));
            }

        }

        [Fact]
        public async void DatabaseHandler_DapperMapping_Unmanaged_Query()
        {
            var mockConnectionFactory = new Mock<IDatabaseConnectionFactory>();
            var db = new InMemoryDatabase();

            mockConnectionFactory.Setup(mcf => mcf.CreateConnection()).Returns(db.OpenConnection());
            SetupMockDatabase(db);

            var handler = new DatabaseHandler(mockConnectionFactory.Object);
            using (var connection = handler.ConnectionFactory.CreateConnection())
            {
                var result = await handler.Query<MockTable>(connection, "SELECT * FROM Mock");

                Assert.True(result.Success);
                Assert.Equal(ConnectionState.Open, mockConnectionFactory.Object.CreateConnection().State);
                Assert.IsType<List<MockTable>>(result.Data);
                Assert.Collection(result.Data, data => Assert.Contains(MOCK_FIRST_ROW, data.Name),
                                              data => Assert.Contains(MOCK_SECOND_ROW, data.Name));
            }
        }
    }
}
