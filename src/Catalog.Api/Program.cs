
using System.Data;
using Catalog.Api.Configuration;
using Catalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);

        IConfiguration config = configurationBuilder.Build();
        var databaseConfiguration = config.GetSection("DatabaseConfiguration").Get<DatabaseConfiguration>();

        if (databaseConfiguration == null)
            throw new ArgumentNullException(nameof(databaseConfiguration));

        // Add services to the container.

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationValidators();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddScoped<IDbConnection>(connection => new SqlConnection(databaseConfiguration.ConnectionString));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
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

