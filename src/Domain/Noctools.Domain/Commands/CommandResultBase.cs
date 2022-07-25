using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Noctools.Domain.Contracts;
using Newtonsoft.Json;

namespace Noctools.Domain.Commands
{
    [ExcludeFromCodeCoverage]
    public abstract class CommandResultBase
    {
        [JsonIgnore] public virtual ValidationState ValidateState { get; set; }
        public virtual IEnumerable<MessageContractDto> Messages { get; set; }

        public virtual bool Success =>
            Messages == null || Messages.All(m => m.Type == MessageType.Information);

        public string ReturnPath { get; set; }
    }
}
