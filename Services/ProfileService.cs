
namespace Happy_Devs_BE.Services
{
    public class ProfileService
    {
        private readonly ProfileRepository _profileRepository;
        public ProfileService(ProfileRepository profileRepository) 
        {
            _profileRepository = profileRepository;
        }

        public Profile GetProfile(int id)
        {
            return _profileRepository.getProfile(id);
        }

        public void UpdateProfile(int id, Profile profile)
        {
            _profileRepository.updateProfile(id, profile);
        }

        public void uploadProfilePicture(int id, IFormFile file)
        {
            byte[] imageData;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                imageData = stream.ToArray();
            }
            _profileRepository.uploadProfilePicture(id, imageData);
        }

        public byte[]? getProfilePicture(int id)
        {
            return _profileRepository.getProfilePicture(id);
        }
    }
}
