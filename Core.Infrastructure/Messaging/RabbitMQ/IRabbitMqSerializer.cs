using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ
{
    public interface IRabbitMqSerializer
    {
        ReadOnlySpan<byte> Serialize(object value);
        object Deserialize(ReadOnlySpan<byte> value, Type type);
        object Deserialize(ReadOnlySpan<byte> value);
    }
}
