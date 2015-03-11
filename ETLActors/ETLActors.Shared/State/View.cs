using System;

namespace ETLActors.Shared.State
{
    public class View : IDataEvent {
        public Guid Id { get; private set; }
        public long Timestamp { get; private set; }
        public string IpAddress { get; private set; }
        public ProductType ProductType { get; private set; }

        public View(Guid id, long timestamp, String ipAddress, ProductType productType)
        {
            Id = id;
            Timestamp = timestamp;
            IpAddress = ipAddress;
            ProductType = productType;
        }

        public View(String ipAddress, ProductType productType) : this(Guid.NewGuid(), DateTime.UtcNow.Ticks, ipAddress, productType)
        {
        }
    }

    /// <summary>
    /// Pageview.
    /// </summary>
    public class Pageview : View {
        public Pageview(Guid id, long timestamp, String ipAddress, ProductType productType) : base(id, timestamp, ipAddress, productType)
        {
        }

        public Pageview(String ipAddress, ProductType productType) : base(ipAddress, productType)
        {
        }

        // TODO add unique visitors hashset? somehow? ID of visitor/session?
    }
}