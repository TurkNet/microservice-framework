using System;
using System.Diagnostics.CodeAnalysis;

namespace Noctools.Domain.Dtos
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class MessageDto
    {
        public MessageDto(string code, string title, string content, MessageType type,
            MessageDisplayLocation displayLocation = MessageDisplayLocation.Top)
        {
            Code = code;
            Title = title;
            Content = content;
            Type = type;
            DisplayLocation = displayLocation;
        }

        public MessageDto(string code, string content, MessageType type,
            MessageDisplayLocation displayLocation = MessageDisplayLocation.Top)
        {
            Code = code;
            Content = content;
            Type = type;
            DisplayLocation = displayLocation;
        }

        public string Title { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public MessageType Type { get; set; }
        public MessageDisplayLocation DisplayLocation { get; set; } = MessageDisplayLocation.Top;
    }
}
