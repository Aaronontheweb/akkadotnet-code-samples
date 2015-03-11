using System;
using Newtonsoft.Json;

namespace ETLActors.Shared.State
{
    public class Payment : IDataEvent {

        [JsonConstructor]
        public Payment(decimal amount, long timestamp, Guid id, Guid orderId)
        {
            Id = id;
            OrderId = orderId;
            Timestamp = timestamp;
            Amount = amount;
        }

        public Payment(decimal amount, Guid orderId) : this(amount, DateTime.UtcNow.Ticks, Guid.NewGuid(), orderId)
        {
        }

        public Guid Id { get; private set; }
        public long Timestamp { get; private set; }
        public decimal Amount { get; private set; }
        public Guid OrderId { get; private set; }
    }
}