namespace Core.Common.Cqrs.Queris;

public interface IQueryHandler<TQuery,TResult> where TQuery : class, IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query,CancellationToken cancellationToken);
}
