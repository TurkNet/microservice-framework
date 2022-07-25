using System;
using Noctools.Utils.Helpers;

namespace Noctools.Domain
{
    public abstract class AggregateRootBase : AggregateRootWithIdBase<Guid>, IAggregateRoot
    {
        protected AggregateRootBase() : base(IdHelper.GenerateId())
        {
        }
    }
}
