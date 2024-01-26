using Happy_Devs_BE.Controllers;

using JWT.Algorithms;
using JWT.Builder;

using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Happy_Devs_BE.Services
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository;
        private readonly UsersAuthenticationService _usersAuthenticationService;

        public UsersService(UsersRepository usersRepository, UsersAuthenticationService usersAuthenticationService)
        {
            _usersRepository = usersRepository;
            _usersAuthenticationService = usersAuthenticationService;
        }

        public int addUser(UserPut newUser)
        {
            newUser.Password = _usersAuthenticationService.GetHashString(newUser.Password);

            return _usersRepository.addUser(newUser);
        }

        public User getUser(int id)
        {
            return _usersRepository.getUser(id);
        }
    }
}
