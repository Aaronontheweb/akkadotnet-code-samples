using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
