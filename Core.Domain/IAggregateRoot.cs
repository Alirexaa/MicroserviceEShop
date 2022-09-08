namespace Core.Domain;

public interface IAggregateRoot<out TKey> : IEntity<TKey>
{
    long Version { get; }
    IReadOnlyCollection<IDomainEvent<TKey>> DomainEvents { get; }
    void ClearDomainEvents();
}
