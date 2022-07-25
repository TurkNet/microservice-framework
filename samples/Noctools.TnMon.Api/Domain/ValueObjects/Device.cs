using System.Collections.Generic;
using Noctools.Domain;
using Noctools.TnMon.Api.Domain.Constants;
using Noctools.Utils;

namespace Noctools.TnMon.Api.Domain
{
    public class Device : ValueObjectBase
    {
        public string Name { get; set; }

        public Device(string name)
        {
            Name = name;
        }

        public static Device Load(string name)
        {
            Guard.That<DomainException>(string.IsNullOrEmpty(name),
                nameof(DomainErrorCodes.EDAnakin1013),
                DomainErrorCodes.EDAnakin1003);

            return new Device(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}