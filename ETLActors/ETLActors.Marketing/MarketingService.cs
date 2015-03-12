using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Runtime.Remoting.Contexts;
using Akka.Actor;
using Akka.Configuration.Hocon;
using Akka.Routing;
using ETLActors.Marketing.Actors;
using ETLActors.Shared.Commands;
using Topshelf;

namespace ETLActors.Marketing
{
    public class MarketingService : ServiceControl
    {
        protected static ActorSystem ETLSystem;

        public bool Start(HostControl hostControl)
        {
            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            var config = section.AkkaConfig;
            ETLSystem = ActorSystem.Create("ETLActorSystem", config);

            // create coordinators to process message types
            ETLSystem.ActorOf<StatsByZipCoordinatorActor>("StatsByZipCoordinator");
            ETLSystem.ActorOf<StatsByProductCoordinatorActor>("StatsByProductCoordinator");
            ETLSystem.ActorOf<StatsByTimeCoordinatorActor>("StatsByTimeCoordinator");
            ETLSystem.ActorOf<StatsTotalActor>("StatsTotalActor");

            var centralRouter = ETLSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "PublisherRouter");
            // subscribe OrderCommander to all Order messages
            var orderCommander = ETLSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "OrderCommander");
            ETLSystem.Scheduler.Schedule(TimeSpan.FromSeconds(1.5), TimeSpan.FromSeconds(10), centralRouter,
                new SubscribeToTopics(new List<Type>() {typeof (OrderMessage)}, orderCommander));
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ETLSystem.Shutdown();
            return true;
        }
    }
}
