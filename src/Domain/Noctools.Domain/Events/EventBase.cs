using System;
using Noctools.Utils.Helpers;

namespace Noctools.Domain
{
    [Serializable]
    public abstract class EventBase : IEvent
    {
        public int EventVersion { get; protected set; } = 1;
        public DateTime OccurredOn { get; protected set; } = DateTimeHelper.GenerateDateTime();
    }
}
