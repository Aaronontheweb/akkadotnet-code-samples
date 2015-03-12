using System;
using System.Collections.Generic;
using Akka.Actor;

namespace ETLActors.Shared.Commands
{
    public class SubscribeToTopics
    {
        public SubscribeToTopics(List<Type> types, ActorRef subscriber)
        {
            Subscriber = subscriber;
            Types = types;
        }

        public List<Type> Types { get; private set; }

        public ActorRef Subscriber { get; private set; }
    }

    public class UnsubscribeFromTopics
    {
        public UnsubscribeFromTopics(List<Type> types, ActorRef subscriber)
        {
            Subscriber = subscriber;
            Types = types;
        }

        public List<Type> Types { get; private set; }

        public ActorRef Subscriber { get; private set; }
    }

    public class UnsusbscribeFromAll
    {
        public UnsusbscribeFromAll(ActorRef subscriber)
        {
            Subscriber = subscriber;
        }

        public ActorRef Subscriber { get; private set; }
    }
}
