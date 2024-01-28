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
            write($"update users set username = @username, title = @title, bio = @bio where id = {id};", new
            {
                username = profile.UserName,
                bio = profile.Bio,
                title = profile.Title,
            });
        }

        public void uploadProfilePicture(int id, byte[] picture)
        {
            write($"update users set profilepicture = @pfp where id = {id}", new
            {
                pfp = picture,
            });
        }

        public byte[]? getProfilePicture(int id)
        {
            byte[]? profileData = readOne<byte[]>($"select profilepicture from users where id = {id};");
            return profileData;
        }

        private class ProfilePicture
        {
            public byte[]? picture;
        }

        private class ProfileData
        {
            public string username { get; set; }
            public string? title { get; set; }
            public string? bio { get; set; }
        }
    }
}
