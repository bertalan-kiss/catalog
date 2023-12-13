using System.Reflection;
using Carting.Api.HostedService;
using Carting.Core;
using Carting.Infrastructure.DataAccess;
using Carting.Infrastructure.Kafka;
using Carting.Infrastructure.Kafka.Messages;
using Microsoft.OpenApi.Models;

namespace Carting.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
        var config = configurationBuilder.Build();

        // Add services to the container.
        builder.Services.AddCoreServices();
        builder.Services.AddDataAccessRepositories();
        builder.Services.AddKafkaConsumer<ItemUpdatedMessage>(config);
        builder.Services.AddHostedService<CartingHostedService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApiVersioning();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Carting API V1"
            });

            c.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "Carting API V2"
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            c.EnableAnnotations();
            //c.OperationFilter<AddRequiredHeaderParameter>();

            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });

        builder.Services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Carting API V1");
                c.SwaggerEndpoint($"/swagger/v2/swagger.json", "Carting API V2");
            });
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

