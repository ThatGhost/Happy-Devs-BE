using Happy_Devs_BE.Services;
using Happy_Devs_BE.Services.Core;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Happy_Devs_BE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;
        public UsersController(UsersRepository usersRepository) { 
            _usersRepository = usersRepository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _usersRepository.getAllUsers();
        }
    }
}
