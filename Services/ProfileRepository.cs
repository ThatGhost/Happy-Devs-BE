using Happy_Devs_BE.Services.Core;

namespace Happy_Devs_BE.Services
{
    public class ProfileRepository : BaseRepository
    {
        public ProfileRepository(ConnectionPool connectionPool) : base(connectionPool)
        { 
        
        }

        public Profile getProfile(int id)
        {
            ProfileData? profileData = readOne<ProfileData>($"select username, title, bio from users where id = {id};");
            if (profileData == null) throw new Exception("Profile doesnt excist");

            return new Profile()
            {
                UserName = profileData.username,
                Title = profileData.title,
                Bio = profileData.bio,
            };
        }

        public void updateProfile(int id, Profile profile)
        {
            write($"update users set username = '{profile.UserName}', title = '{profile.Title}', bio = '{profile.Bio}' where id = {id};");
        }

        private class ProfileData
        {
            public string username { get; set; }
            public string? title { get; set; }
            public string? bio { get; set; }
        }
    }
}
