using Carting.Core.Services;
using Carting.Infrastructure.Kafka.Messages;
using Carting.Infrastructure.Kafka.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Carting.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection collection)
        {
            collection.AddSingleton<ICartingService, CartingService>();
            collection.AddSingleton<IMessageProcessor<ItemUpdatedMessage>, ItemUpdatedMessageProcessor>();
            return collection;
        }
    }
}

