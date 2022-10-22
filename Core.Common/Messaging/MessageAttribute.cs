using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Messaging;

[AttributeUsage(AttributeTargets.Class)]
public class MessageAttribute : Attribute
{
    public string Exchange { get; }
    public string RoutingKey { get; }
    public string Queue { get; }
    public bool External { get; }

    public MessageAttribute(string exchange = null, string routingKey = null, string queue = null,
        bool external = false)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
        Queue = queue;
        External = external;
    }
}
