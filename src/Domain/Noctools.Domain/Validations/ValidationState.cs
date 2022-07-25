namespace Noctools.Domain
{
    public enum ValidationState
    {
        Valid = 1,
        NotAcceptable = 2,
        AlreadyExist = 3,
        DoesNotExist = 4,
        UnProcessable = 5,
        PreconditionRequired = 6,
        PreconditionFailed = 7
    }
}
