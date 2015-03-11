using System;
using Akka.Actor;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    class PaymentByProductCoordinatorActor : ReceiveActor
    {
        public PaymentByProductCoordinatorActor()
        {
            //Receive<PaymentMessage>(msg => Console.WriteLine("product pmt message"));
        }
    }
}
