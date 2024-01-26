namespace Happy_Devs_BE.Services
{
    public class User
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
    }

    public class UserAuthData
    {
        public string email { get; set; }

        public string password { get; set; }
        public int id { get; set; }
    }
}
