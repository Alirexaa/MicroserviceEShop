using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Event
{
    public interface IEventDispatcher
    {
        Task PublishLocal<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
        Task Commit<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}
