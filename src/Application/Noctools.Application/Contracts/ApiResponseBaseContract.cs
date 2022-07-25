using System.Collections.Generic;
using System.Linq;
using Noctools.Domain;
using Noctools.Domain.Contracts;
using Newtonsoft.Json;

namespace Noctools.Application.Dtos
{
    /// <summary>
    /// todo: paginated querylerin fix edilmesi gerekli.
    /// </summary>
    /// <typeparam name="TContractDto"></typeparam>
    public class ApiResponseBaseContract<TContractDto>
    {
        [JsonProperty("instance")] public virtual string Instance { get; set; }
        [JsonProperty("messages")] public virtual List<MessageContractDto> Messages { get; set; }
        [JsonProperty("result")] public virtual TContractDto Result { get; set; }

        [JsonProperty("success")]
        public virtual bool Success => Messages?.All(m => m.Type == MessageType.Information) ?? true;

        [JsonProperty("returnPath")] public virtual string ReturnPath { get; set; }
    }
}