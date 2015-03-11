using System;

namespace ETLActors.Shared.State
{
    public class MarketingCampaign : IIdentifiable {
        public MarketingCampaign(decimal cost, Guid id)
        {
            Id = id;
            Cost = cost;
            Purchases = 0;
        }

        public MarketingCampaign(decimal cost) : this(cost, new Guid())
        {
        }

        public Guid Id { get; private set; }
        public decimal Cost { get; private set; }
        public int Purchases { get; private set; }

        // TODO add increment/decrement purchases
    }
}