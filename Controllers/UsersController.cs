using Happy_Devs_BE.Controllers;
using Happy_Devs_BE.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Happy_Devs_BE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly UsersAuthenticationService _usersAuthenticationService;
        public UsersController(UsersService usersService, UsersAuthenticationService usersAuthenticationService) { 
            _usersService = usersService;
            _usersAuthenticationService = usersAuthenticationService;
        }

        // GET: api/<UsersController>/@id
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _usersService.getUser(id);
        }

        // Put: api/<UsersController>
        [HttpPost()]
        public async Task<int> post([FromBody] UserPut user)
        {
            return await _usersService.addUser(user);
        }

        // Post: api/<UsersController>/login
        [HttpPost("login")]
        public async Task<LoginReponse> login([FromBody] LoginRequest loginData)
        {
            return new LoginReponse()
            {
                token = await _usersAuthenticationService.login(loginData.email, loginData.password)
            };
        }
    }
}
