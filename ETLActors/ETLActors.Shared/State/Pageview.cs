using System;

namespace ETLActors.Shared.State
{
    public class Pageview : IDataEvent {
        public Guid Id { get; private set; }
        public long Timestamp { get; private set; }
        public string IpAddress { get; private set; }
        public Product Product { get; private set; }

        public Pageview(Guid id, long timestamp, String ipAddress, Product product)
        {
            Id = id;
            Timestamp = timestamp;
            IpAddress = ipAddress;
            Product = product;
        }

        public Pageview(String ipAddress, Product product) : this(Guid.NewGuid(), DateTime.UtcNow.Ticks, ipAddress, product)
        {
        }
    }
}