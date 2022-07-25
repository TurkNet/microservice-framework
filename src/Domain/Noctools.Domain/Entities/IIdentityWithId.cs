namespace Noctools.Domain
{
    /// <summary>
    ///     Supertype for all Identity types with generic Id
    /// </summary>
    public interface IIdentityWithId<TId>
    {
        TId Id { get; }
    }
}
