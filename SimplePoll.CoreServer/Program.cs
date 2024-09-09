using Microsoft.EntityFrameworkCore;
using SimplePoll.CoreServer.Data;
using SimplePoll.CoreServer.Extensions;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<SimplePollDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("SimplePollDB")));
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.RegisterRepositories();
        builder.Services.RegisterAutomapperProfiles();

        builder.ConfigureAllowedClientTokens();

        var app = builder.Build();

        app.UseSimpleTokenValidation();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}