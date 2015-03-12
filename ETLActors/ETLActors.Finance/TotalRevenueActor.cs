using Akka.Actor;
using ETLActors.Shared;
using ETLActors.Shared.Commands;

namespace ETLActors.Finance
{
    public class TotalRevenueActor : ReceiveActor
    {
        protected ActorRef CancelledOrders;
        protected ActorRef CompletedOrders;

        public TotalRevenueActor()
        {
            Receive<CreateOrder>(message => ProcessNewOrder(message));
            Receive<CancelOrder>(message => ProcessCancelOrder(message));
        }

        protected override void PreStart()
        {
            CancelledOrders =
                Context.ActorOf(
                    Props.Create(
                        () =>
                            new OrderSumActor(CountActorBase.TimeInterval.Day,
                               "CANCELLED ORDERS ")));

            CompletedOrders =
               Context.ActorOf(
                   Props.Create(
                       () =>
                           new OrderSumActor(CountActorBase.TimeInterval.Day,
                               "COMPLETED ORDERS")));
        }

        private void ProcessCancelOrder(CancelOrder order)
        {
            CancelledOrders.Tell(order);
        }

        private void ProcessNewOrder(CreateOrder order)
        {
            CompletedOrders.Tell(order);
        }
    }
}
