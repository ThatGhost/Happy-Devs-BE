using Happy_Devs_BE.Services.Posts;
using Happy_Devs_BE.Services;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Happy_Devs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly UsersAuthenticationService _usersAuthenticationService;
        private readonly UsersAuthorazationService _usersAuthorazationService;

        public CodeController(
            UsersAuthenticationService usersAuthenticationService,
            UsersAuthorazationService usersAuthorazationService)
        {
            _usersAuthenticationService = usersAuthenticationService;
            _usersAuthorazationService = usersAuthorazationService;
        }

        [HttpPost]
        public async Task AddFile([FromBody] FileRequest request)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);

        }

        [HttpPost]
        public async Task AddFolder([FromBody] FolderRequest request)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);

        }

        [HttpGet]
        public async Task getFolder()
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);

        }
    }
}
