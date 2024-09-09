using SimplePoll.CoreServer.Data.Repositories;
using SimplePoll.CoreServer.Services;

namespace SimplePoll.CoreServer.Extensions
{
    public static class RegisterRepositoriesExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {

            services.AddTransient<IPollRepository, PollRepository>();
            services.AddTransient<ISubmissionRepository, SubmissionRepository>();

            services.AddTransient<IPollService, PollService>();
        }
    }
}
