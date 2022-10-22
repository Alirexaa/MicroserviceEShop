using Core.Common.Messaging;
using System.Reflection;

namespace Core.Infrastructure.Messaging.RabbitMQ
{
    public class Convention : IConvention
    {
        public string Exchage { get; }

        public string Queue { get; }

        public string RoutingKey { get; }

        public Type Type { get; }

        public Convention(Type type, string exchage, string queue, string routingKey)
        {
            Exchage = exchage;
            Queue = queue;
            RoutingKey = routingKey;
            Type = type;
        }
    }

    private static MessageAttribute GeAttribute(MemberInfo type) => type.GetCustomAttribute<MessageAttribute>();
}
