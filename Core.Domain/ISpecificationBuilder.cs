using System.Linq.Expressions;

namespace Core.Domain
{
    public interface ISpecificationBuilder<T>
    {
        ISpecification<T> Specification { get; }
        ISpecificationBuilder<T> Take(int take);
        ISpecificationBuilder<T> Skip(int skip);
        ISpecificationBuilder<T> Where(Expression<Func<T, bool>> expression);
        ISpecificationBuilder<T> OrderBy(Expression<Func<T, object>> expression);
        ISpecificationBuilder<T> OrderByDescending(Expression<Func<T, object>> expression);
        ISpecificationBuilder<T> Include(Expression<Func<T, object>> expression);
    }
}
