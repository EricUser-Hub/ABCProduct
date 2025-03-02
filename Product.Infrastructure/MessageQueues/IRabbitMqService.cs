using RabbitMQ.Client;

namespace Product.Infrastructure.MessageQueues
{
    public interface IRabbitMqService
    {
        Task<IChannel> CreateChannel();
    }
}