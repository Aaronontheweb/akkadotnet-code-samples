using System;
using System.Linq;
using System.Threading;
using Akka.Actor;
using Akka.Routing;
using Akka.Util.Internal;
using ETLActors.Shared;

namespace ETLFrontend
{
    class MessageGeneratorActor : UntypedActor
    {
        private ActorRef _publisherActor;
        private CancellationTokenSource _publishTask;
        private AtomicCounter _printAttempt = new AtomicCounter(0);

        public MessageGeneratorActor(ActorRef publisherActor)
        {
            _publisherActor = publisherActor;
            _publishTask = new CancellationTokenSource();
        }

        protected override void OnReceive(object message)
        {
            // send fake data into the message bus
            Console.WriteLine("Event" + _printAttempt.GetAndAdd(1));
            _publisherActor.Tell(FakeData.MakeMessage());
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
