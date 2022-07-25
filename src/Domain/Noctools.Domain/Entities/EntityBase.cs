using System;
using Noctools.Utils.Helpers;

namespace Noctools.Domain
{
    public abstract class EntityBase : EntityWithIdBase<Guid>
    {
        protected EntityBase() : base(IdHelper.GenerateId())
        {
        }

        protected EntityBase(Guid id) : base(id)
        {
        }
    }
}
