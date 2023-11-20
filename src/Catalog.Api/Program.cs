using System.Data;
using Catalog.Infrastructure;

namespace Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
        var config = configurationBuilder.Build();

        builder.Services.AddControllers();
        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationValidators();
        builder.Services.AddInfrastructureServices(config);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();
        app.UseRouting();

        app.Run();
    }
}

