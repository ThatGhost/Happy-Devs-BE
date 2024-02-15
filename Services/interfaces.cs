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
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Title { get; set; }
        public string? Bio { get; set; }
    }

    public struct Activiy
    {
        public ActivityType Type { get; set; }
        public DateTime At { get; set; }
    }

    public struct PostMinimal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime At { get; set; }
    }

    public struct Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime At { get; set; }
        public List<PostComment> Comments { get; set; }
    }

    public struct PostComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime At { get; set; }
    }

    public enum ActivityType
    {
        UpdatedProfile = 0,
        MadePost = 1,
        CommentedOnPost = 2,
        AskedQuestion = 3,
        AwnseredQuestion = 4,
        UpdatedDocumentation = 5,
    }

    public struct CodeFolder
    {
        public int Id { get; set; }
        public int? folderId { get; set; }
        public string title { get; set; }
        public DateTime At { get; set; }
    }

    public struct CodeFile
    {
        public int Id { get; set; }
        public int? folderId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime At { get; set; }
    }

}
