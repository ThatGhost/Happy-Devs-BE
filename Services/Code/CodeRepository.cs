using Happy_Devs_BE.Services.Core;

namespace Happy_Devs_BE.Services
{
    public class CodeRepository: BaseRepository
    {
        public CodeRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<List<CodeFolder>> getAllFolders()
        {
            List<CodeFolderData> folderData = await read<CodeFolderData>("select id, at, title, folderId from folders");
            return folderData.Select(toCodeFolder).ToList();
        }

        private CodeFolder toCodeFolder(CodeFolderData data)
        {
            return new CodeFolder()
            {
                Id = data.id,
                Title = data.title,
                FolderId = data.folderId,
                At = data.at,
                Files = new List<CodeFile>(),
                Folders = new List<CodeFolder>()
            };
        }

        public async Task<List<CodeFile>> getAllFiles()
        {
            List<CodeFileData> folderData = await read<CodeFileData>("select id, at, title, folderId, content from files");
            return folderData.Select(toCodeFile).ToList();
        }

        private CodeFile toCodeFile(CodeFileData data)
        {
            return new CodeFile()
            {
                Id = data.id,
                Title = data.title,
                At = data.at,
                Content = data.content,
                FolderId = data.folderId,
            };
        }

        public async Task<int> AddFolder(string title, int folderId, DateTime at)
        {
            await write("insert into folders (at, title, folderId) values (@at, @title, @folderId);", new
            {
                at = at,
                title = title,
                folderId = folderId,
            });

            IdGet? newFolderId = await readOne<IdGet>("select id from folders where at = @at AND folderId = @folderId AND title = @title", new
            {
                at = at,
                title = title,
                folderId = folderId,
            });
            if (newFolderId == null) throw new Exception();

            return newFolderId.id;
        }

        public async Task<int> AddFile(string title, int folderId, string content, DateTime at)
        {
            await write("insert into files (at, title, folderId, content) values (@at, @title, @folderId, @content);", new
            {
                at = at,
                title = title,
                folderId = folderId,
                content = content,
            });

            IdGet? newFileId = await readOne<IdGet>("select id from files where at = @at AND folderId = @folderId AND title = @title", new
            {
                at = at,
                title = title,
                folderId = folderId,
            });
            if (newFileId == null) throw new Exception();

            return newFileId.id;
        }

        public async Task DeleteFolders(List<int> folderIds)
        {
            await write($"delete from folders where id in @folderIds;", new
            {
                folderIds = folderIds
            });
        }

        public async Task DeleteFiles(List<int> fileIds)
        {
            await write($"delete from files where id in @fileIds;", new
            {
                fileIds = fileIds
            });
        }

        private class CodeFolderData
        {
            public string title { get; }
            public int id { get; }
            public DateTime at { get; }
            public int? folderId { get; } 
        }

        private class CodeFileData
        {
            public string title { get; }
            public string content { get; }
            public int id { get; }
            public DateTime at { get; }
            public int folderId { get; }
        }
    }
}
