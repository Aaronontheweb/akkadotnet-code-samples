using System;
using System.Collections.Generic;

namespace ETLActors.Shared.Commands
{
    public class SubscribeToTopics
    {
        public SubscribeToTopics(List<Type> types)
        {
            Types = types;
        }

        public List<Type> Types { get; private set; }
    }

    public class UnsubscribeFromTopics
    {
        public UnsubscribeFromTopics(List<Type> types)
        {
            Types = types;
        }

        public List<Type> Types { get; private set; }
    }

    public class UnsusbscribeFromAll { }
}
