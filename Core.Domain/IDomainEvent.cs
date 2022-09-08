namespace Core.Domain;

public interface IDomainEvent<out TKey>
{
    long AggregateVersion { get; }
    TKey AggregateId { get; }
    DateTime When { get; }
}