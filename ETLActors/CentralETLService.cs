using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ETLActors.Actors;
using ETLActors.Shared.Commands;
using ETLActors.Shared.State;
using Topshelf;
using Address = ETLActors.Shared.State.Address;

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
            ETLSystem = ActorSystem.Create("ETLActorSystem");
            SubscriberActor = ETLSystem.ActorOf(Props.Create<SubscriberActor>(), "SubscriberActor");
            PublisherActor = ETLSystem.ActorOf(Props.Create<PublisherActor>(), "PublisherActor");
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
