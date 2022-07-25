using System;

namespace Noctools.Domain
{
    /// <summary>
    ///     Supertype for all Event types
    /// </summary>
    public interface IEvent
    {
        int EventVersion { get; }
        DateTime OccurredOn { get; }
    }
}
