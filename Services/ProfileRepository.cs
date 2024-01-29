using Happy_Devs_BE.Services.Core;

namespace Happy_Devs_BE.Services
{
    public class ProfileRepository : BaseRepository
    {
        public ProfileRepository(IConfiguration config) : base(config)
        { 
        
        }

        public async Task<Profile> getProfile(int id)
        {
            ProfileData? profileData = await readOne<ProfileData>($"select username, title, bio from users where id = {id};");
            if (profileData == null) throw new Exception("Profile doesnt excist");

            return new Profile()
            {
                UserName = profileData.username,
                Title = profileData.title,
                Bio = profileData.bio,
            };
        }

        public async Task updateProfile(int id, Profile profile)
        {
            await write($"update users set username = @username, title = @title, bio = @bio where id = {id};", new
            {
                username = profile.UserName,
                bio = profile.Bio,
                title = profile.Title,
            });
        }

        public async Task uploadProfilePicture(int id, byte[] picture)
        {
            await write($"update users set profilepicture = @pfp where id = {id}", new
            {
                pfp = picture,
            });
        }

        public async Task<byte[]?> getProfilePicture(int id)
        {
            byte[]? profileData = await readOne<byte[]>($"select profilepicture from users where id = {id};");
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
