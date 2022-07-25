using System.Threading;
using System.Threading.Tasks;

namespace Noctools.Domain
{
    public interface IEventHandler<in TEvent, TResult>
        where TEvent : IEvent
    {
        Task<TResult> Handle(TEvent request, CancellationToken cancellationToken);
    }
}
