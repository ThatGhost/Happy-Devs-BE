namespace Happy_Devs_BE.Controllers
{
    public struct ProfileResponse
    {
        public int id {  get; set; }
        public string? bio { get; set; }
        public string username { get; set; }
        public string? title { get; set; }
    }

    public struct ProfileRequest
    {
        public string? bio {  get; set; }
        public string username { get; set; }
        public string? title { get; set; }
    }

    public struct ProfilePicture
    {
        public IFormFile picture { get; set; }
    }
}
