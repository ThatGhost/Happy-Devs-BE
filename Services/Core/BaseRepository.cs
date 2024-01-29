using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.OpenApi.Any;

namespace Happy_Devs_BE.Services.Core
{
    public class BaseRepository
    {
        private readonly string connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        protected async Task<List<T>> read<T>(string query, object? parameters = null) where T : class
        {
            List<T> data = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                data = (await connection.QueryAsync<T>(query, parameters)).ToList();
                await connection.CloseAsync();
            }
            return data;
        }

        protected async Task<T?> readOne<T>(string query, object? parameters = null) where T: class
        {
            T data;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                data = await connection.QueryFirstAsync<T>(query, parameters);
                await connection.CloseAsync();
            }
            return data;
        }

        protected async Task write(string query, object? parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                await connection.QueryAsync(query, parameters);
                await connection.CloseAsync();
            }
        }
    }
}
