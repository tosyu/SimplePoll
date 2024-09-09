using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using SimplePoll.CoreServer.Data.Options;
using System.Linq;

namespace SimplePoll.CoreServer.Middleware
{
    public class SimpleTokenValidationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOptions<AllowedClientTokensOptions> options;

        public SimpleTokenValidationMiddleware(RequestDelegate next, IOptions<AllowedClientTokensOptions> options)
        {
            this.next = next;
            this.options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var enabled = options?.Value.Enable ?? false;

            if (!enabled || context.Request.Path.ToString().StartsWith("/swagger"))
            {
                await next(context);
                return;
            }

            var authorizationHeader = context.Request.Headers["Authorization"].ToString();

            if (authorizationHeader == null)
            {
                await ReturnForbidden(context);
                return;
            }

            var basicToken = authorizationHeader?.Split("Basic ")?.LastOrDefault();
            var allowed = options?.Value?.Tokens ?? [];

            if (string.IsNullOrEmpty(basicToken) || !allowed.Contains(basicToken))
            {
                await ReturnForbidden(context);
                return;
            }

            await next(context);
        }

        public async Task ReturnForbidden(HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
        }
    }
}
