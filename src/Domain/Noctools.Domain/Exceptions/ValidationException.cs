namespace Noctools.Domain
{
    public class ValidationException : CoreException
    {
        public string Code { get; private set; }

        public string UserFriendlyMessage { get; private set; }

        public ValidationException(string message)
            : base(message, null)
        {
        }

        public ValidationException(string code, string message, string userFriendlyMessage = "")
            : base(message, null)
        {
            Code = code;
            UserFriendlyMessage = userFriendlyMessage;
        }

        public ValidationException(string code, string message)
            : base(message, null)
        {
            Code = code;
        }
    }
}
