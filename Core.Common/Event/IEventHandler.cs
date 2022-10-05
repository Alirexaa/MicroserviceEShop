using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Event
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandelAsync(TEvent @event, CancellationToken cancellationToken = default);
    }
}
