using Dapper;

using Happy_Devs_BE.Controllers;
using Happy_Devs_BE.Services.Core;
using Microsoft.Data.SqlClient;

namespace Happy_Devs_BE.Services
{
    public class UsersRepository : BaseRepository
    {
        public UsersRepository(IConfiguration config) : base(config)
        {

        }

        public async Task<User> getUser(int id)
        {
            UserGetData? user = await readOne<UserGetData>($"select username, title, email from users where id = {id};");
            if (user == null) throw new Exception();

            return new User()
            {
                UserName = user.username,
                Title = user.title,
                Email = user.email,
            };
        }

        public async Task<UserAuth> getUserAuthData(string email)
        {
            UserAuthData? userAuthData = await readOne<UserAuthData>($"select password, id, email from users where email = '{email}'");
            if (userAuthData == null) throw new Exception();

            return convertToUserAuth(userAuthData);
        }

        public async Task<UserAuth> getUserAuthData(int id)
        {
            UserAuthData? userAuthData = await readOne<UserAuthData>($"select password, id, email from users where id = {id}");
            if (userAuthData == null) throw new Exception();

            return convertToUserAuth(userAuthData);
        }

        private UserAuth convertToUserAuth(UserAuthData data)
        {
            return new UserAuth()
            {
                Email = data.email,
                Password = data.password,
                Id = data.id,
            };
        }

        public async Task<int> addUser(UserPut user)
        {
            await write($"INSERT INTO users (username, email, password) VALUES ('{user.UserName}', '{user.Email}', '{user.Password}');");
            return (await readOne<IdGet>($"SELECT id FROM users WHERE email = '{user.Email}';"))!.id;
        }

        private class UserGetData
        {
            public string username { get; }
            public string title { get; }
            public string email { get; }
        }

        public class UserAuthData
        {
            public string email { get; set; }

            public string password { get; set; }
            public int id { get; set; }
        }
    }
}
