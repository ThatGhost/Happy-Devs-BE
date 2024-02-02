using Happy_Devs_BE.Services;
using Happy_Devs_BE.Services.Core;
using Happy_Devs_BE.Services.Posts;

namespace Happy_Devs_BE.DiContainer
{
    class DiContainer
    {
        public static void registerServices(IServiceCollection container)
        {
            container.AddTransient<UsersRepository>();
            container.AddTransient<UsersService>();
            container.AddTransient<UsersAuthenticationService>();
            container.AddTransient<ProfileService>();
            container.AddTransient<ProfileRepository>();
            container.AddTransient<UsersAuthorazationService>();
            container.AddTransient<ActivityService>();
            container.AddTransient<ActivityRepository>();
            container.AddTransient<PostsService>();
            container.AddTransient<PostsRepository>();
        }
    }
}
