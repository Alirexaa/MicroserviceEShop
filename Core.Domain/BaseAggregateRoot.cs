using System.Collections.Immutable;
using System.Reflection;

namespace Core.Domain;

public abstract class BaseAggregateRoot<TA, TKey> : BaseEntity<TKey>, IAggregateRoot<TKey>
        where TA : class, IAggregateRoot<TKey>
{
    

    protected BaseAggregateRoot() { }

    protected BaseAggregateRoot(TKey id) : base(id)
    {
    }
    public long Version { get; private set; }


    private List<IDomainEvent<TKey>> _domainEvents;
    public IReadOnlyCollection<IDomainEvent<TKey>> DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(IDomainEvent<TKey> eventItem)
    {
        _domainEvents = _domainEvents ?? new List<IDomainEvent<TKey>>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent<TKey> eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    //protected abstract void When(IDomainEvent<TKey> @event);

    #region Factory

    private static readonly ConstructorInfo CTor;

    static BaseAggregateRoot()
    {
        var aggregateType = typeof(TA);
        CTor = aggregateType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
            null, new Type[0], new ParameterModifier[0]);
        if (null == CTor)
            throw new InvalidOperationException($"Unable to find required private parameterless constructor for Aggregate of type '{aggregateType.Name}'");
    }

    public static TA Create(IEnumerable<IDomainEvent<TKey>> events)
    {
        if (null == events || !events.Any())
            throw new ArgumentNullException(nameof(events));
        var result = (TA)CTor.Invoke(new object[0]);

        var baseAggregate = result as BaseAggregateRoot<TA, TKey>;
        if (baseAggregate != null)
            foreach (var @event in events)
                baseAggregate.AddDomainEvent(@event);

        result.ClearDomainEvents();

        return result;
    }

    #endregion Factory
}