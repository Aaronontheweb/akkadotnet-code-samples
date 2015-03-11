using System;
using Akka.Actor;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    class PaymentByTimeCoordinatorActor : ReceiveActor
    {
        public PaymentByTimeCoordinatorActor()
        {
            //Receive<PaymentMessage>(msg => Console.WriteLine("time pmt message"));
        }
    }
}
