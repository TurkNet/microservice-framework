using System;
using System.Collections.Generic;

namespace Noctools.Domain
{
    public interface IAggregateRootWithId<TId> : IEntityWithId<TId>
    {
        IAggregateRootWithId<TId> ApplyEvent(IEvent payload);
        List<IEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
        IAggregateRootWithId<TId> RemoveEvent(IEvent @event);
        IAggregateRootWithId<TId> AddEvent(IEvent uncommittedEvent);
        IAggregateRootWithId<TId> RegisterHandler<T>(Action<T> handler);
    }
}
