using Happy_Devs_BE.Services;
using Happy_Devs_BE.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Happy_Devs_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostsService _postsService;
        private readonly UsersAuthenticationService _usersAuthenticationService;
        private readonly UsersAuthorazationService _usersAuthorazationService;

        public PostsController(
            PostsService postsService,
            UsersAuthenticationService usersAuthenticationService,
            UsersAuthorazationService usersAuthorazationService)
        { 
            _postsService = postsService;
            _usersAuthenticationService = usersAuthenticationService;
            _usersAuthorazationService = usersAuthorazationService;
        }

        // Post api/<ValuesController>/5
        [HttpPost("{id}")]
        public async Task<int> Post(int id, [FromBody] PostRequest request)
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            _usersAuthorazationService.isUserRequestedUser(Request.Headers, id);
            return await _postsService.createPost(id, request.title, request.content);
        }

        [HttpGet("recent")]
        public async Task<PostResponse[]> GetRecent()
        {
            await _usersAuthenticationService.authenticateUser(Request.Headers);
            Post[] posts = await _postsService.getRecentPosts();
            return posts.Select(p => toPostResponse(p)).ToArray();
        }

        private PostResponse toPostResponse(Post post)
        {
            return new PostResponse()
            {
                id = post.Id,
                userId = post.UserId,
                title = post.Title,
                content = post.Content,
                at = post.At
            };
        }
    }
}
