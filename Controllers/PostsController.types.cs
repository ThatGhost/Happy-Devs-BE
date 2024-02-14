namespace Happy_Devs_BE.Controllers
{
    public struct PostRequest
    {
        public string title { get; set; }
        public string content { get; set; } 
    }

    public struct PostResponse
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime at { get; set; }
        public List<CommentResponse> comments { get; set; }
    }

    public struct CommentResponse
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string content { get; set; }
        public DateTime at { get; set; }
    }

    public struct PostResponseMinimal
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public DateTime at { get; set; }
    }

}
