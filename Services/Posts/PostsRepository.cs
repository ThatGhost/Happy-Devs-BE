using Happy_Devs_BE.Services.Core;

namespace Happy_Devs_BE.Services.Posts
{
    public class PostsRepository: BaseRepository
    {
        public PostsRepository(IConfiguration configuration): base(configuration) { }

        public async Task<int> createPost(int userId, string title, string content, DateTime at)
        {
            await write(@"insert into posts (userId, title, post, at) values (@userId,@title,@content,@at);",
                new
                {
                    userId = userId,
                    title = title,
                    content = content,
                    at = at
                });

            IdGet? newId = await readOne<IdGet>(@"select id from posts where userId = @userId AND at = @at;", new
            {
                userId = userId,
                at = at
            });
            if (newId == null) throw new Exception("post not created");
            return newId.id;
        }
    }
}
