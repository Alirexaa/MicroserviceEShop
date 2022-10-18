using Core.Common.Event;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MS.Catalog.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "catalog";
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task Complate()
        {
            await SaveChangesAsync();
        }
    }
}

public static class EventDispacherExtensions
{
    public static async Task DispatchDomainEventsAsync(this IEventDispatcher eventDispatcher, CatalogDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker.Entries().Where(entry => entry.GetType() == typeof(BaseAggregateRoot<,>)).Cast<BaseAggregateRoot<IAggregateRoot<object>, object>>()
            .Where(x => x.DomainEvents != null && x.DomainEvents.Any());

        //var domainEntities = ctx.ChangeTracker
        //    .Entries<Entity>()
        //    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await eventDispatcher.PublishLocal(domainEvent);
    }
}
