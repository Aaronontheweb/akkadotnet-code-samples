using System;
using System.Net;

namespace ETLActors
{
    public class View : IDataEvent {
        public Guid Id { get; private set; }
        public long Timestamp { get; private set; }
        public IPAddress IpAddress { get; private set; }
        public ProductType ProductType { get; private set; }

        public View(Guid id, long timestamp, IPAddress ipAddress, ProductType productType)
        {
            Id = id;
            Timestamp = timestamp;
            IpAddress = ipAddress;
            ProductType = productType;
        }

        public View(IPAddress ipAddress, ProductType productType) : this(Guid.NewGuid(), DateTime.UtcNow.Ticks, ipAddress, productType)
        {
        }
    }

    public class Pageview : View {
        public Pageview(Guid id, long timestamp, IPAddress ipAddress, ProductType productType) : base(id, timestamp, ipAddress, productType)
        {
        }

        public Pageview(IPAddress ipAddress, ProductType productType) : base(ipAddress, productType)
        {
        }
    }
}