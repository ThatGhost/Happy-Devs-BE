namespace Happy_Devs_BE.Services.Posts
{
    public class PostsService
    {
        private readonly PostsRepository _postsRepository;
        public PostsService(PostsRepository postsRepository)
        { 
            _postsRepository = postsRepository;
        }

        public async Task<int> createPost(int userId, string title, string content)
        {
            return await _postsRepository.createPost(userId, title, content, DateTime.UtcNow);
        }

        public async Task<Post[]> getRecentPosts()
        {
            int limit = 10;
            return await _postsRepository.getRecentPosts(limit);
        }
    }
}
