namespace Happy_Devs_BE.Services
{
    public class CodeService
    {
        private readonly CodeRepository _codeRepository;
        private readonly CodeStructureComposer _composer;
        public CodeService(
            CodeRepository codeRepository,
            CodeStructureComposer codeStructureComposer)
        {
            _codeRepository = codeRepository;
            _composer = codeStructureComposer;
        }

        public async Task<CodeFolder> getRoot()
        {
            List<CodeFolder> folders = await _codeRepository.getAllFolders();
            List<CodeFile> files = await _codeRepository.getAllFiles();

            return _composer.ComposeRootFolder(folders, files);
        }

        public async Task<int> AddFolder(string title, int folderId)
        {
            DateTime now = DateTime.UtcNow;
            return await _codeRepository.AddFolder(title, folderId, now);
        }

        public async Task<int> AddFile(string title, int folderId, string content)
        {
            DateTime now = DateTime.UtcNow;
            return await _codeRepository.AddFile(title, folderId, content, now);
        }

        public async Task<CodeFolder> getFolder(int folderId)
        {
            List<CodeFolder> folders = await _codeRepository.getAllFolders();
            List<CodeFile> files = await _codeRepository.getAllFiles();

            return _composer.ComposeFolder(folderId, folders, files);
        }

        public async Task DeleteFullFolder(int folderId)
        {
            CodeFolder folder = await getFolder(folderId);

            List<int> toDeleteFiles = new List<int>();
            GetFileIdsRecursive(folder, ref toDeleteFiles);
            List<int> toDeleteFolders = new List<int>();
            GetFolderIdsRecursive(folder, ref toDeleteFolders);

            await _codeRepository.DeleteFiles(toDeleteFiles);
            await _codeRepository.DeleteFolders(toDeleteFolders);
        }

        private void GetFolderIdsRecursive(CodeFolder folder, ref List<int> folderIds)
        {
            folderIds.Add(folder.Id);
            for (int i = 0; i < folder.Folders.Count; i++)
            {
                GetFolderIdsRecursive(folder.Folders[i], ref folderIds);
            }
        }

        private void GetFileIdsRecursive(CodeFolder folder, ref List<int> fileIds)
        {
            foreach (CodeFile file in folder.Files)
            {
                fileIds.Add(file.Id);
            }

            for (int i = 0; i < folder.Folders.Count; i++)
            {
                GetFileIdsRecursive(folder.Folders[i], ref fileIds);
            }
        }

        public async Task DeleteFile(int fileId)
        {
            await _codeRepository.DeleteFiles(new List<int>() { fileId });
        }

        public async Task<CodeFile> GetFile(int fileId)
        {
            return await _codeRepository.getFile(fileId);
        }
    }
}
