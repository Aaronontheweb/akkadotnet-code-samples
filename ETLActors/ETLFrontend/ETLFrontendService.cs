using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration.Hocon;
using Akka.Routing;
using Topshelf;

namespace ETLFrontend
{
    class ETLFrontendService : ServiceControl
    {
        protected static ActorSystem ETLSystem;
        protected ActorRef MessageGeneratorActor;

        public bool Start(HostControl hostControl)
        {
            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            var config = section.AkkaConfig;
            ETLSystem = ActorSystem.Create("ETLActorSystem", config);

            var centralRouter = ETLSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "PublisherRouter");

            MessageGeneratorActor = ETLSystem.ActorOf(Props.Create<MessageGeneratorActor>(centralRouter), "MessageGeneratorActor");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ETLSystem.Shutdown();
            return true;
        }
    }
}
