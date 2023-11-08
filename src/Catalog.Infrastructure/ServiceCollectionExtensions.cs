using Catalog.Application.Interfaces;
using Catalog.Domain.Configuration;
using Catalog.Infrastructure.DataAccess.Repositories;
using Catalog.Infrastructure.Kafka;
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
            return collection;
        }
    }
}

