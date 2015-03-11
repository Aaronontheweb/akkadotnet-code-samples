using System;
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
