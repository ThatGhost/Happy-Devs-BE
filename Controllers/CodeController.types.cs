namespace Happy_Devs_BE.Controllers
{
    public struct FolderRequest
    {
        public string title { get; }
        public int folderId { get;}
        public List<FileRequest> files { get; }
    }

    public struct FileRequest
    {
        public int folderId { get; }
        public string title { get; }
        public string content { get; }
    }
}
