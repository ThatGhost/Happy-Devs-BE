using Happy_Devs_BE.Services.Posts;
using Happy_Devs_BE.Services;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Happy_Devs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly UsersAuthenticationService _usersAuthenticationService;
        private readonly UsersAuthorazationService _usersAuthorazationService;
        private readonly CodeService _codeService;

        public CodeController(
            UsersAuthenticationService usersAuthenticationService,
            UsersAuthorazationService usersAuthorazationService,
            CodeService codeService)
        {
            _usersAuthenticationService = usersAuthenticationService;
            _usersAuthorazationService = usersAuthorazationService;
            _codeService = codeService;
        }

        [HttpPost("file")]
        public async Task<int> AddFile([FromBody] FileRequest request)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            return await _codeService.AddFile(request.title, request.folderId, request.content);
        }

        [HttpPost("folder")]
        public async Task<int> AddFolder([FromBody] FolderRequest request)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            return await _codeService.AddFolder(request.title, request.folderId);
        }

        [HttpGet("folder")]
        public async Task getFolder()
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);

        }

        [HttpGet("root")]
        public async Task<FolderResponse> getRoot()
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            return toFolderResponse(await _codeService.getRoot());
        }

        [HttpDelete("folder/{id}")]
        public async Task deleteFolder(int id)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            await _codeService.DeleteFullFolder(id);
        }

        [HttpDelete("file/{id}")]
        public async Task deleteFile(int id)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            await _codeService.DeleteFile(id);
        }

        private FolderResponse toFolderResponse(CodeFolder folder)
        {
            List<FolderResponse> folders = folder.Folders.Select(toFolderResponse).ToList();
            List<FileResponse> files = folder.Files.Select(toFileResponse).ToList();

            return new FolderResponse()
            {
                files = files,
                folders = folders,
                folderId = folder.Id,
                title = folder.Title,
                at = folder.At,
            };
        }

        private FileResponse toFileResponse(CodeFile file)
        {
            return new FileResponse()
            {
                at = file.At,
                title = file.Title,
                content = file.Content,
                fileId = file.Id,
                folderId = file.FolderId,
            };
        }
    }
}
