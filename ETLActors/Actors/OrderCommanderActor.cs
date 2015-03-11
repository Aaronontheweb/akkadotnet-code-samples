using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    class OrderCommanderActor : ReceiveActor
    {
        public OrderCommanderActor()
        {
            Receive<OrderMessage>(msg => Console.WriteLine("received order message"));
        }
    }
}
