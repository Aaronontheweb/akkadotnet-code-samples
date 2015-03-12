using System;
using Akka.Actor;
using ETLActors.Shared;
using ETLActors.Shared.Commands;
using ETLActors.Shared.State;

namespace ETLActors.Marketing.Actors
{
    class StatsByZipWorkerActor : ReceiveActor
    {
        private readonly String _zipCode;

        #region Messages
        public class IdentifyZip
        {
        }

        public class MyZip
        {
            public MyZip(string zip)
            {
                Zip = zip;
            }
            public String Zip { get; private set; }
        }
        #endregion

        protected ActorRef CancelledOrders;
        protected ActorRef CompletedOrders;

        public StatsByZipWorkerActor(string zipCode)
        {
            _zipCode = zipCode;

            Receive<IdentifyZip>(msg => Sender.Tell(new MyZip(_zipCode)));
            Receive<CreateOrder>(message => ProcessNewOrder(message));
            Receive<CancelOrder>(message => ProcessCancelOrder(message));
        }

        protected override void PreStart()
        {
            CancelledOrders =
                Context.ActorOf(
                    Props.Create(
                        () =>
                            new OrderSumActor(CountActorBase.TimeInterval.Hour,
                                string.Format("CANCELLED ORDERS in {0}", _zipCode))));

            CompletedOrders =
               Context.ActorOf(
                   Props.Create(
                       () =>
                           new OrderSumActor(CountActorBase.TimeInterval.Hour,
                               string.Format("COMPLETED ORDERS in {0}", _zipCode))));
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
