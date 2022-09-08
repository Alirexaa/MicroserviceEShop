using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public interface ISpecification<T>
    {
        ISpecificationBuilder<T> Query { get; }
        int? Take { get; set; }
        int? Skip { get; set; }
        List<Expression<Func<T, bool>>> WhereExpressions { get; }
        List<Expression<Func<T, object>>> OrderByExpressions { get; }
        List<Expression<Func<T, object>>> OrderByDescendingExpressions { get; }
        List<Expression<Func<T, object>>> IncludeExpressions { get; }
    }
}
