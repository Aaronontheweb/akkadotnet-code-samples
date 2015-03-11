using System;

namespace ETLActors.State
{
    public class Payment : IDataEvent {
        public Payment(decimal amount, int last4, int binNumber, long timestamp, Guid id)
        {
            Id = id;
            Timestamp = timestamp;
            Amount = amount;
            Last4 = last4;
            BinNumber = binNumber;
        }

        public Payment(decimal amount, int last4, int binNumber) : this(amount, last4, binNumber, DateTime.UtcNow.Ticks, Guid.NewGuid())
        {
        }

        public Guid Id { get; private set; }
        public long Timestamp { get; private set; }
        public decimal Amount { get; private set; }
        public int Last4 { get; private set; }
        public int BinNumber { get; private set; }
    }
}