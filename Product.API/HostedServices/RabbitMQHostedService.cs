using Product.Infrastructure.MessageQueues;

namespace Product.API.HostedServices
{
    public class RabbitMQHostedService(IRabbitListener listenerService) : BackgroundService
    {
        private readonly IRabbitListener rabbitListenerService = listenerService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await rabbitListenerService.ReadMessages();
        }
    }
}