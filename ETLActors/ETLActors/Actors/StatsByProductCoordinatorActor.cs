using Akka.Actor;

namespace ETLActors.Actors
{
    class StatsByProductCoordinatorActor : ReceiveActor
    {
        public StatsByProductCoordinatorActor()
        {
            //Receive<PaymentMessage>(msg => Console.WriteLine("product pmt message"));
        }
    }
}
