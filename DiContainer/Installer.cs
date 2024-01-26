using Happy_Devs_BE.Services;
using Happy_Devs_BE.Services.Core;

namespace Happy_Devs_BE.DiContainer
{
    class DiContainer
    {
        public static void registerServices(IServiceCollection container)
        {
            container.AddSingleton<ConnectionPool>();
            container.AddTransient<UsersRepository>();
            container.AddTransient<UsersService>();
            container.AddTransient<UsersAuthenticationService>();
        }
    }
}
