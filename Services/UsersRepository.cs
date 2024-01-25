using Happy_Devs_BE.Services.Core;
using MapDataReader;
using Microsoft.Data.SqlClient;

namespace Happy_Devs_BE.Services
{
    public class UsersRepository 
    {
        private readonly ConnectionPool connectionPool;
        public UsersRepository(ConnectionPool connectionPool)
        {
            this.connectionPool = connectionPool;
        }

        public string[] getAllUsers()
        {
            SqlConnection conn = connectionPool.getConnection();
            var dataReader = new SqlCommand("select * from testTable;", conn).ExecuteReader();
            List<IdAndName> users = dataReader.ToIdAndName();

            return users.Select(u => u.name).ToArray();
        }
    }
}
