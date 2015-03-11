using Akka.Actor;

namespace ETLActors.Marketing
{
    class StatsByTimeCoordinatorActor : ReceiveActor
    {
        public StatsByTimeCoordinatorActor()
        {
            //Receive<PaymentMessage>(msg => Console.WriteLine("time pmt message"));
        }
    }
}
