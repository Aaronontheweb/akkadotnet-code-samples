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
            // central looks at high-level interfaces to see which actors to subscribe to what

            // subscribe payment commander to all payment messages
            var pmtCommander = Context.System.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "PaymentCommander");
            Context.System.EventStream.Subscribe(pmtCommander, typeof(PaymentMessage));

            var pmtByZipCoord = Context.System.ActorOf<PaymentByZipCoordinatorActor>("PaymentByZipCoordinator");
            var pmtByProductCoord = Context.System.ActorOf<PaymentByProductCoordinatorActor>("PaymentByProductCoordinator");
            var pmtByTimeCoord = Context.System.ActorOf<PaymentByTimeCoordinatorActor>("PaymentByTimeCoordinator");


            // subscribe Order commander to all Order messages
            var orderCommander = Context.System.ActorOf<OrderCommanderActor>("OrderCommander");
            Context.System.EventStream.Subscribe(orderCommander, typeof(OrderMessage));

            // subscribe pageview commander to all pageview messages
            var pageviewCommander = Context.System.ActorOf<PageviewCommanderActor>("PageviewCommander");
            Context.System.EventStream.Subscribe(pageviewCommander, typeof(LogPageview));

            Console.WriteLine("SubscriberActor ready");
        }
    }
}
