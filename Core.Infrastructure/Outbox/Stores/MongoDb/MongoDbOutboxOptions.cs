﻿using Core.Common.Outbox;

namespace Core.Infrastructure.Outbox.Stores.MongoDb
{
    public class MongoDbOutboxOptions : OutboxOptions
    {
        public string CollectionName { get; set; } = "Messages";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } = "OutboxDb";
    }
}
