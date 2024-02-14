namespace Happy_Devs_BE.Services.Posts
{
    public class PostsService
    {
        private readonly PostsRepository _postsRepository;
        private readonly ActivityService _activityService;
        public PostsService(
            PostsRepository postsRepository,
            ActivityService activityService
        )
        { 
            _postsRepository = postsRepository;
            _activityService = activityService;
        }

        public async Task<int> createPost(int userId, string title, string content)
        {
            await _activityService.addActivity(userId, ActivityType.MadePost);
            return await _postsRepository.createPost(userId, title, content, DateTime.UtcNow);
        }

        public async Task<Post[]> getRecentPosts()
        {
            int limit = 10;
            return await _postsRepository.getRecentPosts(limit);
        }

        public async Task<Post> getPost(int id)
        {
            return await _postsRepository.getPost(id);
        }
    }
}
