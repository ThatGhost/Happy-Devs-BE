using Microsoft.Data.SqlClient;

namespace Happy_Devs_BE.Services.Core
{
    public class ConnectionPool
    {
        private readonly ILogger<ConnectionPool> logger;
        private readonly IConfiguration config;

        private List<SqlConnection> sqlConnections = new List<SqlConnection>();

        public ConnectionPool(ILogger<ConnectionPool> logger, IConfiguration configuration)
        {
            this.logger = logger;
            config = configuration;
        }

        public void start()
        {
            for (int i = 0; i < 3; i++)
            {
                SqlConnection connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
                sqlConnections.Add(connection);
            }
            logger.LogInformation("started connections");
        }

        public SqlConnection getConnection()
        {
            foreach (SqlConnection connection in sqlConnections)
            {
                if (connection.State == System.Data.ConnectionState.Open) continue;
                connection.Open();
                return connection;
            }

            SqlConnection newConnection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
            newConnection.Open();
            sqlConnections.Add(newConnection);
            return newConnection;
        }
    }
}
