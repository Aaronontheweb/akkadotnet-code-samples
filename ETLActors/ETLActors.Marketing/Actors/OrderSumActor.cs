using System;
using System.Collections.Generic;
using System.Linq;
using ETLActors.Shared.Commands;
using ETLActors.Shared.State;

namespace ETLActors.Marketing.Actors
{
    public class OrderSumActor : CountActorBase
    {
        protected Dictionary<DateTime, decimal> DistinctSumsPerInterval;

        public OrderSumActor(TimeInterval aggregationInterval, string metricsDescription) 
            : base(aggregationInterval, metricsDescription)
        {
            DistinctSumsPerInterval = new Dictionary<DateTime, decimal>();
            Ready();
        }

        private void Ready()
        {
            Receive<OrderMessage>(orderMessage =>
            {
                CountEvent(orderMessage.Order);
                SumOrder(orderMessage.Order);
            });

            Receive<PublishStatsTick>(tick =>
            {
                PublishCounts();
                PublishSums();
            });
        }

        protected void PublishSums()
        {
            var currentInterval = IntervalCalculator(DateTime.UtcNow.Ticks);
            foreach (var interval in DistinctSumsPerInterval)
            {
                Console.WriteLine("Per-{0} {1} SUM for {2}: {3}", MetricsDescription, AggregationInterval, interval.Key,
                    interval.Value);
            }

            //remove previous intervals
            DistinctSumsPerInterval =
                DistinctSumsPerInterval.Where(x => x.Key != currentInterval)
                    .ToDictionary(key => key.Key, v => v.Value);
        }

        protected void SumOrder(Order order)
        {
            var interval = IntervalCalculator(order.Timestamp);
            if (!DistinctSumsPerInterval.ContainsKey(interval))
            {
                DistinctSumsPerInterval[interval] = 0;
            }
            DistinctSumsPerInterval[interval] = DistinctSumsPerInterval[interval] + order.Payment.Amount;
        }
    }
}