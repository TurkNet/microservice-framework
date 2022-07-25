using Google.Protobuf;
using MediatR;
using Noctools.Domain;

namespace Noctools.Bus
{
    /// <summary>
    /// This class contains the domain event which published from domain entity.
    /// We use DomainEventBus to handle and publish it back to the specific project,
    /// then it will handle and use some of popular event brokers like Redis/Kafka to handle it
    /// </summary>
    public class NotificationEnvelope : INotification
    {
        public NotificationEnvelope(IEvent @event)
        {
            Event = @event;
        }

        public IEvent Event { get; }
    }

    /// <summary>
    /// This class contains the Protobuf message with the idea that we will use Protobuf
    /// for inter-communication bus via event broker like Redis/Kafka
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageEnvelope<T> : INotification
        where T : IMessage<T>
    {
        public MessageEnvelope(IMessage<T> message)
        {
            Message = message;
        }

        public IMessage<T> Message { get; }
    }
}