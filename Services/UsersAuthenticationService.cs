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

        public void authenticateUser(IHeaderDictionary headers)
        {
            const string authHeaderKey = "Authentication";
            if (!headers.ContainsKey(authHeaderKey)) throw new Exception("No authentication header found");
            string token = headers[authHeaderKey];

            if (!compareLoginHash(token)) throw new Exception("Authentication failed");
        }

        private bool compareLoginHash(string incomingToken)
        {
            int id = int.Parse(incomingToken.Substring(0, incomingToken.IndexOf(":")));
            UserAuth userAuthData = _usersRepository.getUserAuthData(id);

            string correctUserAuthToken = GetHashString(userAuthData.Email + userAuthData.Password);
            string incomingUserAuthToken = incomingToken.Substring(incomingToken.IndexOf(':') + 1);

            return correctUserAuthToken == incomingUserAuthToken;
        }

        public string login(string email, string password)
        {
            UserAuth userAuthData = _usersRepository.getUserAuthData(email);
            string newPasswordHash = GetHashString(password);

            if (newPasswordHash != userAuthData.Password) throw new Exception();
            return userAuthData.Id.ToString() + ":" + GetHashString(email + newPasswordHash);
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
