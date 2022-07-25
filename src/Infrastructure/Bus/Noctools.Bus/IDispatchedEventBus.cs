using System.Threading.Tasks;
using Google.Protobuf;

namespace Noctools.Bus
{
    public interface IDispatchedEventBus
    {
        Task PublishAsync<TMessage>(TMessage msg, params string[] channels) where TMessage : IMessage<TMessage>;
        Task SubscribeAsync<TMessage>(params string[] channels) where TMessage : IMessage<TMessage>, new();
    }
}