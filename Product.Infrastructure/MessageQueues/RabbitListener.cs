using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Product.Infrastructure.MessageQueues 
{
    public class RabbitListener : IRabbitListener
    {
        private const string ProductQueue = "ABCProduct";
        private readonly IChannel channel;

        public RabbitListener(IRabbitMqService rabbitMqService)
        {
            channel = rabbitMqService.CreateChannel().GetAwaiter().GetResult();
        }

        // TODO Eric :
        public async Task ReadMessages()
        {
            await channel.QueueDeclareAsync(queue: ProductQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            
            var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" [x] Received {message}");
                    return Task.CompletedTask;
                };

                await channel.BasicConsumeAsync(ProductQueue, autoAck: true, consumer: consumer);
        }
    }
}