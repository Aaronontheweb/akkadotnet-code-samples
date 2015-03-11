using System.Configuration;
using Akka.Actor;
using Akka.Configuration.Hocon;
using Akka.Routing;
using ETLActors.Actors;
using Topshelf;

namespace ETLActors
{
    public class CentralETLService : ServiceControl
    {
        protected static ActorSystem ETLSystem;
        protected ActorRef SubscriberActor;
        protected ActorRef PublisherActor;
        protected ActorRef MessageGeneratorActor;

        public bool Start(HostControl hostControl)
        {
            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            var config = section.AkkaConfig;
            ETLSystem = ActorSystem.Create("ETLActorSystem", config);
            SubscriberActor = ETLSystem.ActorOf(Props.Create<SubscriberActor>(), "SubscriberActor");
            PublisherActor = ETLSystem.ActorOf(Props.Create<PublisherActor>().WithRouter(FromConfig.Instance), "PublisherActor");
            MessageGeneratorActor = ETLSystem.ActorOf(Props.Create<MessageGeneratorActor>(PublisherActor), "MessageGeneratorActor");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ETLSystem.Shutdown();
            return true;
        }
    }
}
