namespace Happy_Devs_BE.Services
{
    public class CodeStructureComposer
    {
        public CodeFolder ComposeRootFolder(List<CodeFolder> folders, List<CodeFile> files)
        {
            CodeFolder root = folders.Find(f => f.FolderId == null);
            folders.Remove(root);

            root = unfoldFolder(root, ref folders, ref files);

            return root;
        }

        public CodeFolder ComposeFolder(int folderId, List<CodeFolder> folders, List<CodeFile> files)
        {
            CodeFolder root = folders.Find(f => f.Id == folderId);
            folders.Remove(root);

            root = unfoldFolder(root, ref folders, ref files);

            return root;
        }

        private CodeFolder unfoldFolder(CodeFolder folder, ref List<CodeFolder> allFolders, ref List<CodeFile> allFiles)
        {
            List<CodeFolder> inFolders = allFolders.FindAll(f => f.FolderId == folder.Id);
            List<CodeFile> filesInFolders = allFiles.FindAll(f => f.FolderId == folder.Id);

            folder.Folders = inFolders;
            folder.Files = filesInFolders;

            allFolders.RemoveAll(r => r.FolderId == folder.Id);
            allFiles.RemoveAll(r => r.FolderId == folder.Id);

            if (folder.Folders.Count != 0)
            {
                for (int i = 0; i < folder.Folders.Count; i++)
                {
                    folder.Folders[i] = unfoldFolder(folder.Folders[i], ref allFolders, ref allFiles);
                }
            }

            return folder;
        }
    }
}
