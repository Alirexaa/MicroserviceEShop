using System.Linq.Expressions;

namespace Core.Domain
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {

            Query = new SpecificationBuilder<T>(this);
        }

        public ISpecificationBuilder<T> Query { get; }


        public List<Expression<Func<T, bool>>> WhereExpressions { get; private set; } = new List<Expression<Func<T, bool>>>();

        public List<Expression<Func<T, object>>> OrderByExpressions { get; private set; } = new List<Expression<Func<T, object>>>();

        public List<Expression<Func<T, object>>> OrderByDescendingExpressions { get; private set; } = new List<Expression<Func<T, object>>>();

        public List<Expression<Func<T, object>>> IncludeExpressions { get; private set; } = new List<Expression<Func<T, object>>>();
        public int? Take { get; set; } = null;
        public int? Skip { get; set; } = null;
    }
}
