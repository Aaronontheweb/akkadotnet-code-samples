using System;
using Akka.Actor;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    class OrderCommanderActor : ReceiveActor
    {
        public OrderCommanderActor()
        {
            Receive<OrderMessage>(msg => Console.WriteLine("received Order message"));
        }
    }
}
