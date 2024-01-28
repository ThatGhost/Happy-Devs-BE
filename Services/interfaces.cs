namespace Happy_Devs_BE.Services
{
    public struct User
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
    }

    public struct UserAuth
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public int Id { get; set; }
    }

    public struct Profile
    {
        public string UserName { get; set; }
        public string? Title { get; set; }
        public string? Bio { get; set; }
    }
}
