namespace Happy_Devs_BE.Services
{
    public class UsersAuthorazationService
    {
        public void isUserRequestedUser(IHeaderDictionary headers, int id)
        {
            const string authHeaderKey = "Authentication";
            if (!headers.ContainsKey(authHeaderKey)) throw new Exception("No authentication header found");
            string token = headers[authHeaderKey];
            int tokenid = int.Parse(token.Substring(0, token.IndexOf(":")));

            if (tokenid != id) throw new Exception("user not authorized");
        }
    }
}
