using Microsoft.Extensions.DependencyInjection;
using SimplePoll.CoreServer.Data.Options;
using SimplePoll.CoreServer.Middleware;

namespace SimplePoll.CoreServer.Extensions
{
    public static class SimpleTokenValidationMiddlewareExtension
    {
        public static IApplicationBuilder UseSimpleTokenValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleTokenValidationMiddleware>();
        }

        public static void ConfigureAllowedClientTokens(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AllowedClientTokensOptions>(
                builder.Configuration.GetSection(nameof(AllowedClientTokensOptions))
            );
        }
    }
}
