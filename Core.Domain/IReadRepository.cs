namespace Core.Domain;

public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<List<T>> GetListBySpecAsync(ISpecification<T> specification, CancellationToken cancellation = default);
}
