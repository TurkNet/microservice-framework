using System;
using System.Threading.Tasks;

namespace Noctools.Domain
{
    public interface IDomainEventDispatcher : IDisposable
    {
        Task Dispatch(IEvent @event);
    }
}
