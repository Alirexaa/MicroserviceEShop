using Core.Domain;
using System.Threading;


namespace MS.Catalog.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity> : IReadRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        private readonly CatalogDbContext _dbContext;

        public BaseRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entry = await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public async Task<TEntity?> FirstOrDefaultBySpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var entity = await new SpecificationEvalator<TEntity>(specification, _dbContext.Set<TEntity>()).FirstOrDefaltAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(keyValues: new object[] { id } ,cancellationToken:cancellationToken);
            return entity;
        }

        public async Task<List<TEntity>> GetListBySpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var entities = await new SpecificationEvalator<TEntity>(specification, _dbContext.Set<TEntity>()).ToListAsync(cancellationToken);
            return entities;
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
        }
    }
}
