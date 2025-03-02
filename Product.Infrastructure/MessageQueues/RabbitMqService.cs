using Microsoft.Extensions.Options;
using Product.Domain.Configurations;
using RabbitMQ.Client;

namespace Product.Infrastructure.MessageQueues
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public async Task<IChannel> CreateChannel()
        {
            ConnectionFactory factory = 
                new()
                {
                    UserName = _configuration.Username!,
                    Password = _configuration.Password!,
                    HostName = _configuration.HostName!,
                    VirtualHost = _configuration.VHost!,
                };
            var connection = await factory.CreateConnectionAsync();
            return await connection.CreateChannelAsync();
        }
    }
}