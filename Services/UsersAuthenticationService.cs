using System.Security.Cryptography;
using System.Text;

namespace Happy_Devs_BE.Services
{
    public class UsersAuthenticationService
    {
        private readonly UsersRepository _usersRepository;
        public UsersAuthenticationService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public bool compareLoginHash(string incomingToken)
        {
            int id = int.Parse(incomingToken.Substring(0, incomingToken.IndexOf(":")));
            UserAuthData userAuthData = _usersRepository.getUserAuthData(id);

            string correctUserAuthToken = GetHashString(userAuthData.email + userAuthData.password);
            string incomingUserAuthToken = incomingToken.Substring(incomingToken.IndexOf(':') + 1);

            return correctUserAuthToken == incomingUserAuthToken;
        }

        public string login(string email, string password)
        {
            UserAuthData userAuthData = _usersRepository.getUserAuthData(email);
            string newPasswordHash = GetHashString(password);

            if (newPasswordHash != userAuthData.password) throw new Exception();
            return userAuthData.id.ToString() + ":" + GetHashString(email + password);
        }

        public string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            string hash = sb.ToString();
            hash += hash.Substring(5, 6);
            hash = hash.Substring(7, 8);
            hash += hash.Substring(2, 2);

            return sb.ToString();
        }

        private byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

    }
}
