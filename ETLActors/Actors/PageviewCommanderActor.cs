using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    class PageviewCommanderActor : ReceiveActor
    {
        public PageviewCommanderActor()
        {
            Receive<LogPageview>(msg => Console.WriteLine("received pageview msg"));

        }
    }
}
