using System;
using System.ComponentModel.DataAnnotations;
using Nest;
using Noctools.Utils.Helpers;

namespace Noctools.Domain
{
    /// <inheritdoc />
    /// <summary>
    ///  Source: https://github.com/VaughnVernon/IDDD_Samples_NET
    /// impedance mismatch anti pattern :(
    /// </summary>
    public abstract class EntityWithIdBase<TId> : IEntityWithId<TId>
    {
        protected EntityWithIdBase(TId id)
        {
            Id = id;
            Created = DateTimeHelper.GenerateDateTime();
        }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        [Key] public TId Id { get; set; }
    }
}
