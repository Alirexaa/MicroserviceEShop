using System.Linq.Expressions;

namespace Core.Domain
{
    public class SpecificationBuilder<T> : ISpecificationBuilder<T>
    {
        public ISpecification<T> Specification { get; }

        public SpecificationBuilder(ISpecification<T> specification)
        {
            Specification = specification;
        }

        public ISpecificationBuilder<T> Include(Expression<Func<T, object>> expression)
        {
            Specification.IncludeExpressions.Add(expression);
            return this;
        }

        public ISpecificationBuilder<T> OrderBy(Expression<Func<T, object>> expression)
        {
            Specification.OrderByExpressions.Add(expression);
            return this;
        }

        public ISpecificationBuilder<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            Specification.OrderByDescendingExpressions.Add(expression);
            return this;
        }

        public ISpecificationBuilder<T> Skip(int skip)
        {
            Specification.Skip = skip;
            return this;
        }

        public ISpecificationBuilder<T> Take(int take)
        {
            Specification.Take = take;
            return this;
        }

        public ISpecificationBuilder<T> Where(Expression<Func<T, bool>> expression)
        {
            Specification.WhereExpressions.Add(expression);
            return this;
        }
    }
}
