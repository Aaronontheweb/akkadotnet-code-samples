using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace ETLActors.Actors
{
    class PaymentByZipWorkerActor : ReceiveActor
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

        public PaymentByZipWorkerActor(string zipCode)
        {
            _zipCode = zipCode;

            Receive<IdentifyZip>(msg => Sender.Tell(new MyZip(_zipCode)));

        }

    }
}
