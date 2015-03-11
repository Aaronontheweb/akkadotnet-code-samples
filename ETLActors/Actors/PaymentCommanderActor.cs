using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    class PaymentCommanderActor : ReceiveActor
    {
        public PaymentCommanderActor()
        {
            Receive<PaymentMessage>(msg => Console.WriteLine("received payment msg"));

        }
    }
}
