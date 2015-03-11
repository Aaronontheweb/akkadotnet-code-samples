using System;
using System.Threading;
using Akka.Actor;
using ETLActors.Shared;

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
            // send fake data into the message bus
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
