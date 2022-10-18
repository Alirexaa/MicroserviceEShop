using Core.Common.Cqrs.Commands;
using Core.Common.Event;
using Core.Common.Extenstions;
using Core.Common.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MS.Catalog.Infrastructure.Data;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Catalog.Infrastructure.Behaviours
{
    public class TransactionBehaviour<TCommand, TResult> : ICommandPipelineBehavior<TCommand, TResult>
    {
        private readonly CatalogDbContext _dbContext;
        private readonly ILogger<TransactionBehaviour<TCommand,TResult>> _logger;
        private readonly IOutboxListener _outboxListener;
        public TransactionBehaviour(CatalogDbContext dbContext, ILogger<TransactionBehaviour<TCommand, TResult>> logger, IOutboxListener outboxListener)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(CatalogDbContext));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
            _outboxListener = outboxListener ?? throw new ArgumentException(nameof(IOutboxListener));
        }

        public async Task<TResult> HandelAsync(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate<TResult> next)
        {
            var response = default(TResult);
            var typeName = command.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using var transaction = await _dbContext.BeginTransactionAsync();
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, command);

                        response = await next();

                        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                   
                    //await _orderingIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, command);

                throw;
            }
        }
    }
}
