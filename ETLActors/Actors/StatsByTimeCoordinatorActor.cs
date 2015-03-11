using Akka.Actor;

namespace ETLActors.Actors
{
    class StatsByTimeCoordinatorActor : ReceiveActor
    {
        public StatsByTimeCoordinatorActor()
        {
            //Receive<PaymentMessage>(msg => Console.WriteLine("time pmt message"));
        }
    }
}
