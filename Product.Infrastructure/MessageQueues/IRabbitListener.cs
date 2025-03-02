namespace Product.Infrastructure.MessageQueues
{
    public interface IRabbitListener
    {
        Task ReadMessages();
    }
}