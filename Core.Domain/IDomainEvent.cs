namespace Core.Domain;

public interface IDomainEvent<out TKey> : IEvent
{
    long AggregateVersion { get; }
    TKey AggregateId { get; }
    DateTime When { get; }
}