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

        public async Task commentOnPost(int userId, int postId, string content, DateTime at)
        {
            await write(@"insert into post_comments (userId, postId, content, at) values (@userId,@postId,@content,@at);",
                new
                {
                    userId = userId,
                    postId = postId,
                    content = content,
                    at = at
                });
        }

        public async Task<PostMinimal[]> getRecentPosts(int limit = int.MaxValue)
        {
            List<PostData> postsData = await read<PostData>(@$"
                    select top {limit} id, userId, title, at, content
                    from posts 
                    order by at desc;");

            return postsData.Select(x => toPostMinimal(x)).ToArray();
        }

        public async Task<Post> getPost(int id)
        {
            PostData? postsData = await readOne<PostData>(@$"
                    select id, userId, title, at, content
                    from posts 
                    WHERE id = {id}");

            if (postsData == null) throw new Exception("post not found");
            List<PostComment> comments = await getPostComments(id);

            return toPost(postsData, comments);
        }

        public async Task<List<PostComment>> getPostComments(int postId)
        {
            List<PostCommentData> postComments = await read<PostCommentData>($"select id, userId, content, at from post_comments where postId = {postId};");
            return postComments.Select(x => toPostComment(x)).ToList();
        }

        private Post toPost(PostData data, List<PostComment> comments)
        {
            return new Post()
            {
                Id = data.id,
                UserId = data.userId,
                Title = data.title,
                Content = data.content,
                At = data.at,
                Comments = comments
            };
        }

        private PostMinimal toPostMinimal(PostData data)
        {
            return new PostMinimal()
            {
                Id = data.id,
                UserId = data.userId,
                Title = data.title,
                At = data.at,
            };
        }

        private PostComment toPostComment(PostCommentData data)
        {
            return new PostComment()
            {
                Id = data.id,
                UserId = data.userId,
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

        private class PostCommentData
        {
            public int id { get; set; }
            public int userId { get; set; }
            public string content { get; set; }
            public DateTime at { get; set; }
        }
    }
}
