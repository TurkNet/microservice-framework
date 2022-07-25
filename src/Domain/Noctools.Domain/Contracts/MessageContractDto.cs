using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Noctools.Domain.Contracts
{
    [ExcludeFromCodeCoverage]
    public class MessageContractDto
    {
        [JsonProperty("id")]
        public Guid Id { get; } = Guid.NewGuid();
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType Type { get; set; }

        [JsonProperty("displayType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageDisplayLocation DisplayLocation { get; set; } = MessageDisplayLocation.Top;
    }
}
