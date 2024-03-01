namespace Happy_Devs_BE.Controllers
{
    public struct FolderRequest
    {
        public string title { get; set; }
        public int folderId { get; set; }
    }

    public struct FullFolderRequest
    {
        public string title { get; set; }
        public int folderId { get; set; }
        public List<FileRequest> files { get; set; }
        public List<FolderRequest> folder { get; set; }
    }

    public struct FileRequest
    {
        public int folderId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }

    public struct FileResponse
    {
        public int folderId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int fileId { get; set; }
        public DateTime at { get; set; }
    }

    public struct MinimalFileResponse
    {
        public int folderId { get; set; }
        public string title { get; set; }
        public int fileId { get; set; }
        public DateTime at { get; set; }
    }

    public struct FolderResponse
    {
        public int folderId { get; set; }
        public string title { get; set; }
        public DateTime at { get; set; }
        public List<MinimalFileResponse> files { get; set; }
        public List<FolderResponse> folders { get; set; }
    }

}
