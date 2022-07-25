namespace Noctools.Domain
{
    /// <inheritdoc />
    /// <summary>
    ///  Supertype for all Entity types
    /// </summary>
    public interface IEntityWithId<TId> : IIdentityWithId<TId>
    {
    }
}
