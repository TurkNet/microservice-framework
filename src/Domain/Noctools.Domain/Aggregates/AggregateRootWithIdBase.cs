using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Nest;
using Noctools.Utils.Helpers;

namespace Noctools.Domain
{
    public abstract class AggregateRootWithIdBase<TId> : EntityWithIdBase<TId>, IAggregateRootWithId<TId>
    {
        private readonly IDictionary<Type, Action<object>> _handlers = new ConcurrentDictionary<Type, Action<object>>();
        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        protected AggregateRootWithIdBase() : this(default)
        {
        }

        protected AggregateRootWithIdBase(TId id) : base(id)
        {
            Created = DateTimeHelper.GenerateDateTime();
        }

        [Text(Ignore = true)] public int Version { get; protected set; }

        public IAggregateRootWithId<TId> AddEvent(IEvent uncommittedEvent)
        {
            _uncommittedEvents.Add(uncommittedEvent);
            ApplyEvent(uncommittedEvent);
            return this;
        }

        public IAggregateRootWithId<TId> ApplyEvent(IEvent payload)
        {
            if (!_handlers.ContainsKey(payload.GetType()))
                return this;
            _handlers[payload.GetType()]?.Invoke(payload);
            Version++;
            return this;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        public List<IEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents;
        }

        /// <summary>
        /// The communication between of Aggregates makes with events that's why use this register !
        /// https://udidahan.com/2009/06/14/domain-events-salvation/
        /// </summary>
        /// <param name="handler"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IAggregateRootWithId<TId> RegisterHandler<T>(Action<T> handler)
        {
            _handlers.Add(typeof(T), e => handler((T)e));
            return this;
        }

        public IAggregateRootWithId<TId> RemoveEvent(IEvent @event)
        {
            if (_uncommittedEvents.Find(e => e == @event) != null)
                _uncommittedEvents.Remove(@event);
            return this;
        }
    }
}
