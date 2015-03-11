using System;

namespace ETLActors
{
    public class Order : IDataEvent
    {
        public long Timestamp { get; private set; }
        public decimal Total { get; private set; }
        public Guid Id { get; private set; }
        public Customer Customer { get; private set; }
        public Payment Payment { get; private set; }
        public Address Address { get; private set; }
        public MarketingCampaign MarketingCampaign { get; private set; }

        public Order(decimal total, Customer customer, Payment payment, Address address, MarketingCampaign marketingCampaign) : this(DateTime.UtcNow.Ticks, total, customer, payment, address, marketingCampaign, Guid.NewGuid())
        {
        }

        public Order(long timestamp, decimal total, Customer customer, Payment payment, Address address, MarketingCampaign marketingCampaign, Guid guid)
        {
            Timestamp = timestamp;
            Total = total;
            Customer = customer;
            Payment = payment;
            Address = address;
            MarketingCampaign = marketingCampaign;
            Id = guid;
        }

    }
}