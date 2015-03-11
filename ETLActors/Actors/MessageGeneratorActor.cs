using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using ETLActors.Shared.Commands;
using ETLActors.Shared.State;

namespace ETLActors.Actors
{
    class MessageGeneratorActor : UntypedActor
    {
        private ActorRef _publisherActor;
        private CancellationTokenSource _publishTask;

        public MessageGeneratorActor(ActorRef publisherActor)
        {
            _publisherActor = publisherActor;
            _publishTask = new CancellationTokenSource();
        }

        protected override void OnReceive(object message)
        {
            _publisherActor.Tell(new CapturePayment(new Payment(25, 1234, 23)));
            _publisherActor.Tell(new LogPageview(new Pageview("127.0.0.1", new Laptop())));
        }

        protected override void PreStart()
        {
            Context.System.Scheduler.Schedule(TimeSpan.FromMilliseconds(30), TimeSpan.FromMilliseconds(30), Self, "message");
        }

        protected override void PostStop()
        {
            _publishTask.Cancel();
            base.PostStop();
        }
    }
}
