using Happy_Devs_BE.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Happy_Devs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly UsersAuthenticationService _usersAuthenticationService;
        private readonly UsersAuthorazationService _usersAuthorazationService;

        public ProfileController(
            ProfileService profileService,
            UsersAuthenticationService usersAuthenticationService,
            UsersAuthorazationService usersAuthorazationService
            ) 
        {
            _profileService = profileService;
            _usersAuthenticationService = usersAuthenticationService;
            _usersAuthorazationService = usersAuthorazationService;
        }


        [HttpGet("{id}")]
        public async Task<ProfileResponse> get(int id)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);

            Profile profile = await _profileService.GetProfile(id);
            return new ProfileResponse
            {
                title = profile.Title,
                bio = profile.Bio,
                username = profile.UserName,
            };
        }

        [HttpPut("{id}")]
        public async Task put(int id, [FromBody] ProfileRequest request)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            _usersAuthorazationService.isUserRequestedUser(Request.Headers, id);

            await _profileService.UpdateProfile(id, new Profile()
            {
                UserName = request.username,
                Title = request.title,
                Bio = request.bio,
            });
        }

        [HttpPut("{id}/pfp")]
        public async Task putPfP(int id, IFormFile file)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            _usersAuthorazationService.isUserRequestedUser(Request.Headers, id);

            await _profileService.uploadProfilePicture(id, file);
        }

        [HttpGet("{id}/pfp")]
        public async Task<IActionResult?> getPfP(int id)
        {
            byte[]? bytes = await _profileService.getProfilePicture(id);
            if (bytes == null) return null;
            return File(bytes, "image/jpeg", "profilePicture");
        }
    }
}
