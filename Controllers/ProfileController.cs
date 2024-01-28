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
        public ProfileResponse get(int id)
        {
            _usersAuthenticationService.authenticateUser(Request.Headers);

            Profile profile = _profileService.GetProfile(id);
            return new ProfileResponse
            {
                title = profile.Title,
                bio = profile.Bio,
                username = profile.UserName,
            };
        }

        [HttpPut("{id}")]
        public void put(int id, [FromBody] ProfileRequest request)
        {
            _usersAuthenticationService.authenticateUser(Request.Headers);
            _usersAuthorazationService.isUserRequestedUser(Request.Headers, id);

            _profileService.UpdateProfile(id, new Profile()
            {
                UserName = request.username,
                Title = request.title,
                Bio = request.bio,
            });
        }

        [HttpPut("{id}/pfp")]
        public void putPfP(int id, IFormFile file)
        {
            _usersAuthenticationService.authenticateUser(Request.Headers);
            _usersAuthorazationService.isUserRequestedUser(Request.Headers, id);

            _profileService.uploadProfilePicture(id, file);
        }

        [HttpGet("{id}/pfp")]
        public IActionResult? getPfP(int id)
        {
            byte[]? bytes = _profileService.getProfilePicture(id);
            if (bytes == null) return null;
            return File(bytes, "image/jpeg", "profilePicture");
        }
    }
}
