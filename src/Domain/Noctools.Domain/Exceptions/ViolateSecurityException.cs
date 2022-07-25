namespace Noctools.Domain
{
    public class ViolateSecurityException : CoreException
    {
        public ViolateSecurityException(string message)
            : base(message, null)
        {
        }
    }
}
