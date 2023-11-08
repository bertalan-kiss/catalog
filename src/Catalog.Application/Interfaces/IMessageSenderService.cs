using Catalog.Domain.Messages;
namespace Catalog.Application.Interfaces
{
    public interface IMessageSenderService
    {
        Task SendAsync<T>(T message);
    }
}
