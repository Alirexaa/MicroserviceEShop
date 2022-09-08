namespace Core.Domain;

public abstract class BaseEntity<TKey> : IEntity<TKey>
{
    protected BaseEntity()
    {

    }
    public TKey Id { get; private set; }
    public BaseEntity(TKey id)
    {
        Id = id;
    }
}