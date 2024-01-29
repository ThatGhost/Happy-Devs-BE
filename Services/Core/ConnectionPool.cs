using Microsoft.Data.SqlClient;

namespace Happy_Devs_BE.Services.Core
{
    public class ConnectionPool
    {
        private readonly ILogger<ConnectionPool> logger;
        private readonly IConfiguration config;

        private List<SqlPoolConnection> sqlConnections = new List<SqlPoolConnection>();

        public ConnectionPool(ILogger<ConnectionPool> logger, IConfiguration configuration)
        {
            this.logger = logger;
            config = configuration;
        }

        public void start()
        {
            for (int i = 0; i < 10; i++)
            {
                SqlConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
                sqlConnections.Add(new SqlPoolConnection()
                {
                    conn = connection,
                    isOpen = false,
                });
            }
            logger.LogInformation("started connections");
        }

        public async Task<SqlPoolConnection> getConnection()
        {
            for (int i = 0; i < sqlConnections.Count; i ++)
            {
                SqlPoolConnection connection = sqlConnections[i];
                if (connection.isOpen) continue;
                connection.isOpen = true;
                await connection.conn.OpenAsync();
                return connection;
            }

            SqlConnection newConnection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
            newConnection.Open();

            SqlPoolConnection sqlPoolConnection = new SqlPoolConnection() { conn = newConnection, isOpen = true };
            sqlConnections.Add(sqlPoolConnection);
            return sqlPoolConnection;
        }

        public async Task closeConnection(SqlPoolConnection connection)
        {
            await connection.conn.CloseAsync();
            connection.isOpen = false;
        }

        public struct SqlPoolConnection
        {
            public SqlConnection conn;
            public bool isOpen;
        }
    }
}
