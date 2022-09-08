namespace Core.Domain;

public interface IRepository<T> : IReadRepository<T> where T:class
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> AddRangeAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
}
