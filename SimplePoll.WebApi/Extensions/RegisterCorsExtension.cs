using SimplePoll.WebApi.Data.Options;

namespace SimplePoll.WebApi.Extensions
{
    public static class RegisterCorsExtension
    {
        public static void RegisterCors(this IServiceCollection services, ConfigurationManager configuration)
        {
            var corsOptions = new CorsOptions();

            configuration.GetSection(nameof(CorsOptions)).Bind(corsOptions);

            services.AddCors(options =>
            {
                options.AddPolicy(name: nameof(CorsOptions),
                    policy =>
                    {
                        policy.WithOrigins(corsOptions.Origin);
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                    });
            });
        }

        public static IApplicationBuilder UseCorsWithOptions(this IApplicationBuilder builder)
        {
            return builder.UseCors(nameof(CorsOptions));
        }

    }
}
