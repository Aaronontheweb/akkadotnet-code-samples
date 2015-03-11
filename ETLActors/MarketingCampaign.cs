using System;

namespace ETLActors
{
    public class MarketingCampaign : IIdentifiable {
        public MarketingCampaign(Guid id)
        {
            Id = id;
        }

        public MarketingCampaign() : this(new Guid())
        {
        }

        public Guid Id { get; private set; }
    }
}