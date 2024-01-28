using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.OpenApi.Any;

namespace Happy_Devs_BE.Services.Core
{
    public class BaseRepository
    {
        private readonly ConnectionPool connectionPool;
        public BaseRepository(ConnectionPool connectionPool)
        {
            this.connectionPool = connectionPool;
        }

        protected virtual List<T> read<T>(string query, object? parameters = null) where T : class
        {
            SqlConnection conn = connectionPool.getConnection();
            List<T> data = conn.Query<T>(query, parameters).ToList();
            conn.Close();
            return data;
        }

        protected virtual T? readOne<T>(string query, object? parameters = null) where T: class
        {
            SqlConnection conn = connectionPool.getConnection();
            T data = conn.QueryFirst<T>(query, parameters);
            conn.Close();
            return data;
        }

        protected virtual void write(string query, object? parameters = null)
        {
            SqlConnection conn = connectionPool.getConnection();
            conn.Query(query, parameters);
            conn.Close();
        }
    }
}
