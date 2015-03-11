using System;
using System.Linq;
using System.Threading;
using Akka.Actor;
using Akka.Routing;
using ETLActors.Shared;

namespace ETLFrontend
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
            var res = _publisherActor.Ask<Routees>(new GetRoutees()).Result.Members.Cast<ActorSelectionRoutee>().ToList();
            foreach (var i in res)
            {
                Console.WriteLine(i.Selection.ToString());
            }
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
