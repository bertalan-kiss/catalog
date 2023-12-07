using Catalog.Application.Interfaces;
using Catalog.Domain.Configuration;
using Catalog.Infrastructure.DataAccess.Repositories;
using Catalog.Infrastructure.GraphQl;
using Catalog.Infrastructure.Kafka;
using GraphQL;
using GraphQL.Types;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Catalog.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection collection, IConfiguration configuration)
        {
            var databaseConfiguration = configuration.GetSection("DatabaseConfiguration").Get<DatabaseConfiguration>();
            var kafkaConfiguration = configuration.GetSection("KafkaConfiguration");

            if (databaseConfiguration == null)
            {
                throw new ArgumentNullException(nameof(databaseConfiguration));
            }

            if (kafkaConfiguration == null)
            {
                throw new ArgumentNullException(nameof(kafkaConfiguration));
            }

            collection.AddScoped<ICategoryRepository, CategoryRepository>();
            collection.AddScoped<IItemRepository, ItemRepository>();
            collection.AddScoped<IMessageSenderService, MessageSenderService>();
            collection.AddScoped<IDbConnection>(connection => new SqlConnection(databaseConfiguration.ConnectionString));
            collection.Configure<KafkaConfiguration>(kafkaConfiguration);

            collection.AddScoped<ICategoryGraphQlService, CategoryGraphQlService>();
            collection.AddScoped<CategoryDetailsType>();
            collection.AddScoped<CategoryQuery>();
            collection.AddScoped<ISchema, CategoryDetailsSchema>();
            collection.AddGraphQL(b => b
                .AddAutoSchema<CategoryQuery>()  // sc            
                .AddSystemTextJson());   // serializer
                

            collection.AddScoped<IItemGraphQlService, ItemGraphQlService>();
            collection.AddScoped<ItemDetailsType>();
            collection.AddScoped<ItemQuery>();
            collection.AddScoped<ISchema, ItemDetailsSchema>();
            collection.AddGraphQL(b => b
                .AddAutoSchema<ItemQuery>()  // schema
                .AddSystemTextJson());   // serializer

            return collection;
        }
    }
}

