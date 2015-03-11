using System;
using Akka.Actor;
using Akka.Routing;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    public class SubscriberActor : ReceiveActor
    {
        protected override void PreStart()
        {
            // create coordinators to process message types
            Context.System.ActorOf<StatsByZipCoordinatorActor>("StatsByZipCoordinator");
            Context.System.ActorOf<StatsByProductCoordinatorActor>("StatsByProductCoordinator");
            Context.System.ActorOf<StatsByTimeCoordinatorActor>("StatsByTimeCoordinator");
            Context.System.ActorOf<StatsTotalActor>("StatsTotalActor");


            // subscribe OrderCommander to all Order messages
            var orderCommander = Context.System.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "OrderCommander");
            Context.System.EventStream.Subscribe(orderCommander, typeof(OrderMessage));

            Console.WriteLine("SubscriberActor ready");
        }
    }
}
