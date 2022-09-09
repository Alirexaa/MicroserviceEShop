namespace Core.Domain;

public interface IRepository<T> : IReadRepository<T> where T:class
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    void UpdateRange(List<T> entities);
}
