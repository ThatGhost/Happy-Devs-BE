using Dapper;

using Happy_Devs_BE.Controllers;
using Happy_Devs_BE.Services.Core;
using Microsoft.Data.SqlClient;

namespace Happy_Devs_BE.Services
{
    public class UsersRepository : BaseRepository
    {
        public UsersRepository(ConnectionPool connectionPool) : base(connectionPool)
        {

        }

        public User getUser(int id)
        {
            UserGet? user = readOne<UserGet>($"select username, title, email from users where id = {id};");
            if (user == null) throw new Exception();

            return new User()
            {
                UserName = user.username,
                Title = user.title,
                Email = user.email,
            };
        }

        public int addUser(UserPut user)
        {
            write($"INSERT INTO users (username, title, email, password) VALUES ('{user.UserName}', '{user.Title}', '{user.Email}', '{user.Password}');");
            return readOne<IdGet>($"SELECT id FROM users WHERE email = '{user.Email}';")!.id;
        }

        private class UserGet
        {
            public string username { get; }
            public string title { get; }
            public string email { get; }
        }
    }
}
