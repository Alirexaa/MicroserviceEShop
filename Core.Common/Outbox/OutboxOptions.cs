﻿namespace Core.Common.Outbox
{
    public class OutboxOptions
    {
        public string OutboxType { get; set; }
        public bool DeleteAfter { get; set; }
    }
}
