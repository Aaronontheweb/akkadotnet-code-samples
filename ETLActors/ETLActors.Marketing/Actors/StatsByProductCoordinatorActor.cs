using Akka.Actor;

namespace ETLActors.Marketing.Actors
{
    class StatsByProductCoordinatorActor : ReceiveActor
    {
        public StatsByProductCoordinatorActor()
        {
            //Receive<PaymentMessage>(msg => Console.WriteLine("product pmt message"));
        }
    }
}
