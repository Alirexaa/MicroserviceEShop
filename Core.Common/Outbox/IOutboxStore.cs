﻿namespace Core.Common.Outbox
{
    public interface IOutboxStore
    {
        Task Add(OutboxMessage message);
        Task<IEnumerable<Guid>> GetUnprocessedMessageIds();
        Task SetMessageToProcessed(Guid id);
        Task Delete(IEnumerable<Guid> ids);
        Task<OutboxMessage> GetMessage(Guid id);
    }
}
