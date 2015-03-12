using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Akka.Actor;
using ETLActors.Shared;
using ETLActors.Shared.Commands;
using ETLActors.Shared.State;

namespace ETLActors.Marketing.Actors
{
    public abstract class CountActorBase : ReceiveActor
    {
        protected CancellationTokenSource ReportTask;
        protected Func<long,DateTime> IntervalCalculator;
        protected Dictionary<DateTime, int> DistinctCountsPerInterval;
        protected readonly string MetricsDescription;

        #region Messages

        public class PublishStatsTick { }

        #endregion

        protected CountActorBase(TimeInterval aggregationInterval, string metricsDescription)
        {
            DistinctCountsPerInterval = new Dictionary<DateTime, int>();
            ReportTask = new CancellationTokenSource();
            AggregationInterval = aggregationInterval;
            MetricsDescription = metricsDescription;
            IntervalCalculator = GetCurrentIntervalSelector(aggregationInterval);
        }

        #region Message-handling and aggregation

        protected void PublishCounts()
        {
            var currentInterval = IntervalCalculator(DateTime.UtcNow.Ticks);
            foreach (var interval in DistinctCountsPerInterval)
            {
                Console.WriteLine("Per-{0} {1} COUNT for {2}: {3}", MetricsDescription, AggregationInterval, interval.Key,
                    interval.Value);
            }

            //remove previous intervals
            DistinctCountsPerInterval =
                DistinctCountsPerInterval.Where(x => x.Key != currentInterval)
                    .ToDictionary(key => key.Key, v => v.Value);
        }

        protected void CountEvent(IDataEvent @event)
        {
            var interval = IntervalCalculator(@event.Timestamp);
            if (!DistinctCountsPerInterval.ContainsKey(interval))
            {
                DistinctCountsPerInterval[interval] = 0;
            }
            DistinctCountsPerInterval[interval] = DistinctCountsPerInterval[interval] + 1;
        }

        #endregion

        #region ActorBase methods

        protected override void PreStart()
        {
            Context.System.Scheduler.Schedule(GetReportInterval(AggregationInterval),
                GetReportInterval(AggregationInterval), Self, new PublishStatsTick(), ReportTask.Token);
        }

        protected override void PostStop()
        {
            //cancel publishing
            ReportTask.Cancel();
        }

        #endregion

        #region Reporting plumbing

        public enum TimeInterval
        {
            Minute,
            Hour,
            Day
        }

        public TimeInterval AggregationInterval { get; private set; }

        public static Func<long, DateTime> GetCurrentIntervalSelector(TimeInterval aggregationInterval)
        {
            switch (aggregationInterval)
            {
                case TimeInterval.Hour:
                    return ticks => ticks.ToHour();
                case TimeInterval.Day:
                    return ticks => ticks.ToDay();
                case TimeInterval.Minute:
                default:
                    return ticks => ticks.ToMinute();
            }
        }

        public static TimeSpan GetReportInterval(TimeInterval aggregationInterval)
        {
            switch (aggregationInterval)
            {
                case TimeInterval.Minute:
                    return TimeSpan.FromSeconds(5);
                case TimeInterval.Hour:
                    return TimeSpan.FromSeconds(15);
                case TimeInterval.Day:
                    return TimeSpan.FromSeconds(30);
                default:
                    return TimeSpan.FromSeconds(5);
            }
        }

        #endregion
    }

    public class OrderSumActorBase : CountActorBase
    {
        protected Dictionary<DateTime, decimal> DistinctSumsPerInterval;

        public OrderSumActorBase(TimeInterval aggregationInterval, string metricsDescription) 
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
