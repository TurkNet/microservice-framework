using Noctools.Domain;
using Noctools.Domain.Dtos;

namespace Noctools.Domain
{
    public class ValidationMessage : MessageDto
    {
        public ValidationMessage(string code, string title, string content, MessageType type,
            MessageDisplayLocation displayLocation = MessageDisplayLocation.Top) : base(code, title, content, type,
            displayLocation)
        {
        }

        public ValidationMessage(string code, string content, MessageType type,
            MessageDisplayLocation displayLocation = MessageDisplayLocation.Top) : base(code, content, type,
            displayLocation)
        {
        }
    }
}
