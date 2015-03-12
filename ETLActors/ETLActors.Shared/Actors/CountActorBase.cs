using System;
using System.Collections.Generic;
using System.Threading;
using Akka.Actor;
using ETLActors.Shared.State;

namespace ETLActors.Shared
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
                Console.WriteLine("Per-{0} {1} COUNT for {2}: {3}", AggregationInterval, MetricsDescription, interval.Key.ToShortTimeString(),
                    interval.Value);
            }
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
}
