using System;
using System.Collections.Generic;
using System.Configuration;
using Akka.Actor;
using Akka.Configuration.Hocon;
using Akka.Routing;
using ETLActors.Shared.Commands;
using Topshelf;

namespace ETLActors.Finance
{
    public class FinanceService : ServiceControl
    {
        protected static ActorSystem ETLSystem;

        public bool Start(HostControl hostControl)
        {
            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            var config = section.AkkaConfig;
            ETLSystem = ActorSystem.Create("ETLActorSystem", config);

            // create coordinators to process message types
            ETLSystem.ActorOf<TotalRevenueActor>("StatsTotalActor");

            var centralRouter = ETLSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "SubscriberRouter");
            // subscribe OrderCommander to all Order messages
            var orderCommander = ETLSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "OrderCommander");
            ETLSystem.Scheduler.Schedule(TimeSpan.FromSeconds(1.5), TimeSpan.FromSeconds(10), centralRouter,
                new SubscribeToTopics(new List<Type>() { typeof(OrderMessage) }, orderCommander));
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ETLSystem.Shutdown();
            return true;
        }
    }
}
