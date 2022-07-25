using System;

namespace Noctools.Domain
{
    public interface IAggregateRoot : IAggregateRootWithId<Guid>
    {
    }
}
