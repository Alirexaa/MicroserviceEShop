using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Catalog.Infrastructure.Data
{
    public class SpecificationEvalator<T> where T : class
    {
        private readonly IQueryable<T> _query;

        public SpecificationEvalator(ISpecification<T> specification, IQueryable<T> query)
        {
            foreach (var expression in specification.IncludeExpressions)
            {
                query = query.Include(expression);
            }
            foreach (var expression in specification.WhereExpressions)
            {
                query = query.Where(expression);
            }
            foreach (var expression in specification.OrderByExpressions)
            {
                query = query.OrderBy(expression);
            }
            foreach (var expression in specification.OrderByDescendingExpressions)
            {
                query = query.OrderByDescending(expression);
            }
            if (specification.Take is not null)
            {
                query = query.Take(specification.Take.Value);
            }
            if (specification.Skip is not null)
            {
                query = query.Take(specification.Skip.Value);
            }
            _query = query;
        }
    
        public async Task<List<T>> ToListAsync(CancellationToken cancellation)
        {
            return await _query.ToListAsync(cancellation);
        }
        public async Task<T?> FirstOrDefaltAsync(CancellationToken cancellation)
        {
            return await _query.FirstOrDefaultAsync(cancellation);
        }
    }
}
