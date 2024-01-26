using Happy_Devs_BE.Controllers;

using System.Security.Cryptography;
using System.Text;

namespace Happy_Devs_BE.Services
{
    public class UsersService
    {
        private readonly UsersRepository _usersRepository;

        public UsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public int addUser(UserPut newUser)
        {
            newUser.Password = GetHashString(newUser.Password);

            return _usersRepository.addUser(newUser);
        }

        public User getUser(int id)
        {
            return _usersRepository.getUser(id);
        }

        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
