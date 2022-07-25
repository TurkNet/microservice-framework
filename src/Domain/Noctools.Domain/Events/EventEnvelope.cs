namespace Noctools.Domain
{
    public class EventEnvelope : EventBase
    {
        public EventEnvelope(IEvent @event)
        {
            Event = @event;
        }

        public IEvent Event { get; }
    }
}
