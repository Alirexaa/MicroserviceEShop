namespace Core.Infrastructure.Messaging.RabbitMQ
{
    public interface IConvention
    {
        public Type Type { get;  }
        public string Exchange { get; }
        public string Queue { get; }
        public string RoutingKey { get; }
    }
}
