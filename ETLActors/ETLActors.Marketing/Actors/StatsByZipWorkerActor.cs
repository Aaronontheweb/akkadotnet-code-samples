using System;
using Akka.Actor;
using ETLActors.Shared.Commands;
using ETLActors.Shared.State;

namespace ETLActors.Marketing
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

        public StatsByZipWorkerActor(string zipCode)
        {
            _zipCode = zipCode;

            Receive<IdentifyZip>(msg => Sender.Tell(new MyZip(_zipCode)));
            Receive<CreateOrder>(message => ProcessNewOrder(message.Order));
            Receive<CancelOrder>(message => ProcessCancelOrder(message.Order));


        }

        private void ProcessCancelOrder(Order order)
        {
            Console.WriteLine("zip worker {0} received CANCEL order, amount: {1}", _zipCode, order.Payment.Amount);
            
        }

        private void ProcessNewOrder(Order order)
        {
            Console.WriteLine("zip worker {0} received new order, amount: {1}", _zipCode, order.Payment.Amount);
        }
    }
}
