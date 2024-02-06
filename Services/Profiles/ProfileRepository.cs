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
            List<Profile> profiles = await getProfiles(new int[] {id});
            if (profiles.Count == 0) throw new Exception("Profile does not excist");

            return profiles[0];
        }

        public async Task<List<Profile>> getProfiles(int[] ids)
        {
            List<ProfileData> profileData = await read<ProfileData>($"select id, username, title, bio from users where id in (@ids);", new
            {
                ids = ids
            });

            return profileData.Select(toProfile).ToList();
        }

        private Profile toProfile(ProfileData profileData)
        {
            return new Profile()
            {
                UserName = profileData.username,
                Title = profileData.title,
                Bio = profileData.bio,
                Id = profileData.id,
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
            public int id { get; set; }
            public string username { get; set; }
            public string? title { get; set; }
            public string? bio { get; set; }
        }
    }
}
