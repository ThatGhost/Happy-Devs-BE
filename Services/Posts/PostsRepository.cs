using Happy_Devs_BE.Services.Core;

namespace Happy_Devs_BE.Services.Posts
{
    public class PostsRepository: BaseRepository
    {
        public PostsRepository(IConfiguration configuration): base(configuration) { }

        public async Task<int> createPost(int userId, string title, string content, DateTime at)
        {
            await write(@"insert into posts (userId, title, content, at) values (@userId,@title,@content,@at);",
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

        public async Task<Post[]> getRecentPosts(int limit = int.MaxValue)
        {
            List<PostData> postsData = await read<PostData>(@$"
                    select top {limit} id, userId, title, at, content
                    from posts 
                    order by at desc;");

            return postsData.Select(x => toPost(x)).ToArray();
        }

        public async Task<Post> getPost(int id)
        {
            PostData? postsData = await readOne<PostData>(@$"
                    select id, userId, title, at, content
                    from posts 
                    WHERE id = {id}");

            if (postsData == null) throw new Exception("post not found");

            return toPost(postsData);
        }

        private Post toPost(PostData data)
        {
            return new Post()
            {
                Id = data.id,
                UserId = data.userId,
                Title = data.title,
                Content = data.content,
                At = data.at,
            };
        }

        private class PostData
        {
            public int id { get; set; }
            public int userId { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public DateTime at { get; set; }
        }

    }
}
