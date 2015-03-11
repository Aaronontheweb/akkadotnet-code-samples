using Akka.Actor;

namespace ETLActors.Actors
{
    class PublisherActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {

            Context.System.EventStream.Publish(message);
        }
    }
}
