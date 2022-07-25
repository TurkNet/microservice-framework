using System.Threading.Tasks;

namespace Noctools.Domain
{
    public class MemoryDomainEventDispatcher : IDomainEventDispatcher
    {
        public void Dispose()
        {
        }

        public Task Dispatch(IEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
