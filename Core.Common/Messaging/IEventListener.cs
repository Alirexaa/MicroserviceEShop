using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Messaging
{
    public interface IEventListener
    {
        void Subscribe(Type type);
        void Subscribe<TEvent>() where TEvent : IEvent;
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;  
    }
}
