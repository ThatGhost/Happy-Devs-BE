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
        public UsersController(UsersService usersService) { 
            _usersService = usersService;
        }

        // GET: api/<UsersController>/@id
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _usersService.getUser(id);
        }

        // Put: api/<UsersController>
        [HttpPost]
        public int put([FromBody] UserPut user)
        {
            return _usersService.addUser(user);
        }
    }
}
