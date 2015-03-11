using System;

namespace ETLActors.Shared.State
{
    public class Order : IDataEvent
    {
        public long Timestamp { get; private set; }
        public Guid Id { get; private set; }
        public Payment Payment { get; set; }
        public Address Address { get; private set; }
        public Product Product { get; private set; }

        public Order(Address address, Product product) : this(DateTime.UtcNow.Ticks, address, Guid.NewGuid(), product)
        {
        }

        public Order(long timestamp, Address address, Guid guid, Product product)
        {
            Timestamp = timestamp;
            Address = address;
            Id = guid;
            Product = product;
        }

    }
}