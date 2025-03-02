namespace Product.Domain.Configurations 
{
    public class RabbitMqConfiguration
    {
        public string? VHost { get; set; }
        public string? HostName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}