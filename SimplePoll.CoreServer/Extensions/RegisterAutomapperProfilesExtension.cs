using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SimplePoll.CoreServer.Extensions
{
    public static class RegisterAutomapperProfilesExtension
    {
        public static void RegisterAutomapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}
