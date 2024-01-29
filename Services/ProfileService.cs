
namespace Happy_Devs_BE.Services
{
    public class ProfileService
    {
        private readonly ProfileRepository _profileRepository;
        public ProfileService(ProfileRepository profileRepository) 
        {
            _profileRepository = profileRepository;
        }

        public async Task<Profile> GetProfile(int id)
        {
            return await _profileRepository.getProfile(id);
        }

        public async Task UpdateProfile(int id, Profile profile)
        {
            await _profileRepository.updateProfile(id, profile);
        }

        public async Task uploadProfilePicture(int id, IFormFile file)
        {
            byte[] imageData;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                imageData = stream.ToArray();
            }
            await _profileRepository.uploadProfilePicture(id, imageData);
        }

        public async Task<byte[]?> getProfilePicture(int id)
        {
            return await _profileRepository.getProfilePicture(id);
        }
    }
}
