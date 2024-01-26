namespace Happy_Devs_BE.Controllers
{
    public class UserPut
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginReponse
    {
        public string token { get; set; }
    }
}
