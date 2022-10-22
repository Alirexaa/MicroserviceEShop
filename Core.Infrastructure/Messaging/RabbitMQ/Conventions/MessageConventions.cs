using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Conventions;

public class MessageConventions : IConvention
{
    public Type Type { get; }
    public string RoutingKey { get; }
    public string Exchange { get; }
    public string Queue { get; }
    public MessageConventions(Type type, string routingKey, string exchange, string queue)
    {
        Type = type;
        RoutingKey = routingKey;
        Exchange = exchange;
        Queue = queue;
    }
}
