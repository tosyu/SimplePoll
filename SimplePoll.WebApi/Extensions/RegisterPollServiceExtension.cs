using Microsoft.Net.Http.Headers;
using SimplePoll.WebApi.Data.Options;
using SimplePoll.WebApi.Services;

namespace SimplePoll.WebApi.Extensions
{
    public static class RegisterPollServiceExtension
    {
        public static void RegisterPollService(this IServiceCollection services, ConfigurationManager configuration)
        {
            var options = new CoreServerApiOptions();

            configuration.GetSection(nameof(CoreServerApiOptions)).Bind(options);

            var uri = new Uri(options.Url);
            services.AddHttpClient<IPollService, PollService>(httpClient =>
            {
                httpClient.BaseAddress = uri;
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Basic {options.Token}");
            });
        }
    }
}
