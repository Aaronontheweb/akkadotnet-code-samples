using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using ETLActors.Shared.Commands;

namespace ETLActors.Actors
{
    public class SubscriberActor : ReceiveActor
    {
        protected override void PreStart()
        {
            // subscribe payment commander to all payment messages
            var pmtCommander = Context.System.ActorOf<PaymentCommanderActor>("PaymentCommander");
            Context.System.EventStream.Subscribe(pmtCommander, typeof(PaymentMessage));

            // subscribe order commander to all order messages
            var orderCommander = Context.System.ActorOf<OrderCommanderActor>("OrderCommander");
            Context.System.EventStream.Subscribe(orderCommander, typeof(OrderMessage));

            // subscribe pageview commander to all pageview messages
            var pageviewCommander = Context.System.ActorOf<PageviewCommanderActor>("PageviewCommander");
            Context.System.EventStream.Subscribe(pageviewCommander, typeof(LogPageview));

            Console.WriteLine("SubscriberActor done booting up");
        }
    }
}
