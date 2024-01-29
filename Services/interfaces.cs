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

    public struct Activiy
    {
        public ActivityType Type { get; set; }
        public DateTime At { get; set; }
    }

    public enum ActivityType
    {
        UpdatedProfile = 0,
        MadePost = 1,
        AskedQuestion = 2,
        AwnseredQuestion = 3,
        UpdatedDocumentation = 4,
    }
}
