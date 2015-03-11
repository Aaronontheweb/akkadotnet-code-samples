using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Topshelf;

namespace ETLActors
{
    public class CentralETLService : ServiceControl
    {
        private ActorSystem ETLSystem;

        public bool Start(HostControl hostControl)
        {
            ETLSystem = ActorSystem.Create("ETLActorSystem");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ETLSystem.Shutdown();
            return true;
        }
    }
}
